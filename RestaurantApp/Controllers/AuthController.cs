using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using RestaurantAppCommon.Dtos;
using RestaurantAppCommon.HelperModels;
using RestaurantAppData.DbModels;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantApp.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase //Login,Register,Token işlemleri...
    {
        private readonly ILogger<AuthController> _logger; //Microsoft Core'un sağladığı yapının tanımlanması
        private readonly IPasswordHasher<ApplicationUser> _hasher; //Microsoft'un sağladığı kullanıcı şifresini gizleyerek (hashlemek) tutmak 
        private readonly IConfigurationRoot _config; //appsettings.json'a ulaşmak istediğimizde kullanacağız içindeki ifadeleri okuyabilmek için
        private readonly UserManager<ApplicationUser> _userManager; //Microsoft Identity sağlar
        private readonly SignInManager<ApplicationUser> _signInManager;
        public AuthController(UserManager<ApplicationUser> userManager, 
            ILogger<AuthController> logger, 
            IPasswordHasher<ApplicationUser> hasher, 
            IConfigurationRoot config,
            SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager; //login,register veya token işlemlerinde userManager üzerinden işlem gerçekleştirilir
            _logger = logger; //inject etme
            _hasher = hasher;
            _config = config;
            _signInManager = signInManager;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] CredentialModelDto model) // Login methodu
        {
            try
            {
                var result = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, false, false); // asenkron fonsiyonların burayı atlamaması için await anahtar kelimesini kullandık
                if (result.Succeeded)
                {
                    var user = await _userManager.FindByNameAsync(model.UserName); //kullanıcı isminden kontrol et
                    if (user != null) //user null dan farklı ise
                    {
                        var tokenPacket = CreateToken(user);
                        if (tokenPacket != null && tokenPacket.Result.Token != null)
                        {
                            return Ok(tokenPacket);
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                _logger.LogError($"Login işlemi yapılırken hata oluştu : { ex }");
            }
            return BadRequest("Login başarılı olamadı. Lütefen bilgilerinizi kontrol ediniz.");
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModelDto model)
        {
            if (!ModelState.IsValid) //geçerli değil ise
            {
                return BadRequest("Giriş değerleri hatalıdır.");
            }
            try
            {
                var user = await _userManager.FindByNameAsync(model.UserName); // modelden gelen username database de varsa getir
                if (user != null)
                {
                    return BadRequest("Bu kullanıcı zaten mevcut.");
                }
                else
                {
                    user = new ApplicationUser //Kullanıcı bilgilerini set etme
                    {
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        UserName = model.UserName,
                        Email = model.Email
                    };
                    var result = await _userManager.CreateAsync(user, model.Password); //Kullanıcı oluşturma
                    if (result.Succeeded)
                    {
                        return Ok(CreateToken(user));//kullanıcı oluşturma başarılı ise user bilgisini vererek token oluşturma
                    }
                    else
                    {
                        return BadRequest(result.Errors);
                    }
                }
            }
            catch (Exception ex)
            {

                _logger.LogError($"Kayıt esnasında exception hatası alındı : { ex } ");
                return BadRequest($"Yeni kullanıcı kaydı esnasında hata alındı. Hata : { ex } ");
            }
        }

        //Yaratılan Tokenı kullanmak için gerekli olan Post aksiyonunu yaratacak method.
        //Post işlemi birden çok parametre gönderileceği için frombody içinde yapılır.
        [HttpPost("Token")] //api/Controllerismi/Token
        public async Task<IActionResult> CreateToken([FromBody] CredentialModelDto model)
        {
            try
            {
                var user = await _userManager.FindByNameAsync(model.UserName);
                if (user != null)
                {
                    if (_hasher.VerifyHashedPassword(user, user.PasswordHash, model.Password)==PasswordVerificationResult.Success) //Bu işlem sağlıklı ise
                    {
                        return Ok(CreateToken(user));//Token yarat
                    }

                }
            }
            catch (Exception ex)
            {

                _logger.LogError($"JWT yaratırken bir hata oluştu: {ex.Message.ToString()}");
            }
            return null;
        }

        /// <summary>
        /// Create Token Private Methods
        /// </summary>
        /// <param name="appUser"></param>
        /// <returns></returns>
        private async Task<JwtTokenPacket> CreateToken(ApplicationUser appUser) //asenkron tipinde Task ı olan jwt token packet classında geri dönüş hali olan CreateToken adında bir metot tanımlama
        {
            var userClaims = await _userManager.GetClaimsAsync(appUser); //mevcutta var olan bir claimi getirme
            var claims = new[] //oluşturulan claimler
            {
                new Claim(JwtRegisteredClaimNames.Sub,appUser.UserName), //user name i burada kullan
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()), //yeni guid oluştur
            }.Union(userClaims); //Union ile mevcut olan claimleri oluşturduğumuz claimler ile birleştirme işelemi yaptık

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Token:Key"])); //appsettings.json içindeki key'i, StmmetricSecurty alıp kendisine simetrik bir değer oluşturur bu değeri byte olarak ister
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer: _config["Token:Issuer"],
                audience: _config["Token:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(20), //20 dk geçerli
                signingCredentials: cred);
            return new JwtTokenPacket
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token), //verilen token değerini serialize ederek geriye döndürür
                Expiration = token.ValidTo.ToString(), //Token da bulunan validto değerini string olarak dmndürme
                UserName = appUser.UserName
            };
        
        
        }

     }

}
