using Microsoft.AspNetCore.Mvc;
using RestaurantAppBusinessEngine.Contracts;
using RestaurantAppCommon.Dtos;
using System.Collections.Generic;

namespace RestaurantApp.Controllers
{
    [Route("api/Order")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderBusinessEngine _orderBusinessEngine;
        public OrderController(IOrderBusinessEngine orderBusinessEngine)
        {
            _orderBusinessEngine = orderBusinessEngine;
        }

        [HttpGet("GetOrders")]
        public List<GetOrderDto> GetOrders()
        {
            var result = _orderBusinessEngine.GetOrders();
            return result.Data;
        }

        [HttpPost("SaveOrder")]
        public bool SaveOrder([FromBody]OrderDto order)
        {
            var result = _orderBusinessEngine.SaveOrder(order);
            return result.IsSuccess;
        }

        [HttpDelete("DeleteOrder/{id}")]
        public bool DeleteOrder(int id)
        {
            var data = _orderBusinessEngine.DeleteOrder(id);
            return true;
        }
    }
}
