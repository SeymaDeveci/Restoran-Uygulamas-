using Microsoft.AspNetCore.Identity;
using RestaurantAppBusinessEngine.Contracts;
using RestaurantAppCommon.Dtos;
using RestaurantAppCommon.ResultConstant;
using RestaurantAppData.DbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantAppBusinessEngine.İmplementation
{
    public class ApplicationUserBusinessEngine : IApplicationUserBusinessEngine
    {
        private UserManager<ApplicationUser> _userManager;  //User işlemlerini yapan katmanın ismi:UserManager
        private SignInManager<ApplicationUser> _signInManager;

        public ApplicationUserBusinessEngine(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager) //consturoctor tanımla: katmanlar arası bağımlılıkları en aza indirmek,dependency injection.
        {
            _userManager = userManager; //parametreleri inject etme
            _signInManager = signInManager; //parametreleri inject etme
        }

        public async Task<Result<object>> CreateApplicationUser(ApplicationUserDto model)
        {
            var applicationUser = new ApplicationUser()
            {
                UserName = model.UserName,
                Email = model.Email,
                FullName = model.FullName,
            };

            try
            {
                var result = await _userManager.CreateAsync(applicationUser, model.Password);
                if (result.Errors.Count() > 0)
                    return new Result<object>(false, ResultConstant.RecordNoCreated, result);

                return new Result<object>(true, ResultConstant.RecordCreated, result);
            }
            catch (Exception)
            {

                return new Result<object>(false, ResultConstant.RecordNoCreated);
            }

        }
    }
}
