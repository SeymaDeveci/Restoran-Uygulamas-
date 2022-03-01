using RestaurantAppCommon.Dtos;
using RestaurantAppCommon.ResultConstant;
using System.Collections.Generic;

namespace RestaurantAppBusinessEngine.Contracts
{
    public interface IOrderBusinessEngine 
    {
        Result<bool> SaveOrder(OrderDto order);
        Result<List<GetOrderDto>> GetOrders();
        Result<bool> DeleteOrder(int id);
    }
}
