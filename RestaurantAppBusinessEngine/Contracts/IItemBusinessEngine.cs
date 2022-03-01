using RestaurantAppCommon.Dtos;
using RestaurantAppCommon.ResultConstant;
using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantAppBusinessEngine.Contracts
{
    public interface IItemBusinessEngine
    {
        Result<List<ItemsDto>> GetItems();
    }
}
