using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantAppCommon.Dtos
{
    public class OrderItemModelDto
    {
        public int? OrderItemId { get; set; }
        public string ItemId { get; set; }
        public int Quantity { get; set; }
        public string ItemName { get; set; }
        public decimal? Price { get; set; }
        public int Total { get; set; }
    }
}
