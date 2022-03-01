using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantAppCommon.Dtos
{
    public class ItemsDto
    {
        public int ItemId { get; set; }
        public string Name { get; set; }
        public decimal? Price { get; set; }
    }
}
