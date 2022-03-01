using RestaurantAppCommon.Dtos;
using RestaurantAppCommon.ResultConstant;
using RestaurantAppData.DbModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantAppBusinessEngine.Contracts
{
    public interface IApplicationUserBusinessEngine 
    {
        Task<Result<object>> CreateApplicationUser(ApplicationUserDto model);
    }
}
