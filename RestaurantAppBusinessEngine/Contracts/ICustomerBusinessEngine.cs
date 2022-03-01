using RestaurantAppCommon.Dtos;
using RestaurantAppCommon.ResultConstant;
using System.Collections.Generic;

namespace RestaurantAppBusinessEngine.Contracts
{
    public interface ICustomerBusinessEngine
    {
        Result<List<CustomerDto>> GetCustomers();
    }
}
