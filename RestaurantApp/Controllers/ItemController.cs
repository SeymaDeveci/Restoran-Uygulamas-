using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestaurantAppBusinessEngine.Contracts;
using RestaurantAppCommon.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantApp.Controllers
{
    [Route("api/Item")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        private readonly IItemBusinessEngine _itemBusinessEngine;
        public ItemController(IItemBusinessEngine itemBusinessEngine)
        {
            _itemBusinessEngine = itemBusinessEngine;
        }
        [HttpGet("GetItems")]
        public List<ItemsDto> GetItems()
        {
            return _itemBusinessEngine.GetItems().Data;
        }
    }
}
