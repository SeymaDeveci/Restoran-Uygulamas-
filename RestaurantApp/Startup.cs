using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RestaurantAppBusinessEngine.Contracts;
using RestaurantAppBusinessEngine.Ýmplementation;
using RestaurantAppData.DataContracts;
using RestaurantAppData.DataContext;
using RestaurantAppData.DbModels;
using RestaurantAppData.Ýmplementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RestaurantApp.Controllers.Helper;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace RestaurantApp
{
    public class Startup
    {
        [Obsolete]
        public Startup(Microsoft.AspNetCore.Hosting.IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
          .SetBasePath(env.ContentRootPath)
          .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
          .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
          .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton(Configuration);
            services.AddControllers();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IItemBusinessEngine, ItemBusinessEngine>();
            services.AddScoped<ICustomerBusinessEngine, CustomerBusinessEngine>();
            services.AddScoped<IOrderBusinessEngine, OrderBusinessEngine>();
            services.AddDbContext<RestaurantAppDbContext>(
                options => options.UseSqlServer(Configuration.GetConnectionString("IdentityConnection"))); //Database baðlantýsý kuruldu
            
            services.AddIdentity<ApplicationUser, IdentityRole>().AddDefaultTokenProviders().AddEntityFrameworkStores<RestaurantAppDbContext>();
            services.AddScoped<IApplicationUserBusinessEngine, ApplicationUserBusinessEngine>();
            services.AddCors();

            var appSettingSection = Configuration.GetSection("Token"); //appsettings deki Token bilgisini alma
            services.Configure<AppSettings>(appSettingSection);

            //JWT Authentication
            var appSettings = appSettingSection.Get<AppSettings>(); //Helper> AppSettings classýný getir
            var key = Encoding.ASCII.GetBytes(appSettings.Key); //AppSettings sýnýfýndaki key deðerini alma
            services.AddAuthentication(au =>
            {
                au.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                au.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(jwt =>
            {
                jwt.RequireHttpsMetadata = false;
                jwt.SaveToken = true;
                jwt.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            services.AddControllers().AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling =
            Newtonsoft.Json.ReferenceLoopHandling.Ignore);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors(options =>
            {
                options.WithOrigins("http://localhost:4200") //buna izin ver
                .AllowAnyMethod() //herhangi bir methoda kýsýtlama yapma
                .AllowAnyHeader() //herhangi bir isteðe takýlma
                .AllowCredentials(); //herhangi bir kontrol de yapma
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();
            app.UseAuthentication();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
