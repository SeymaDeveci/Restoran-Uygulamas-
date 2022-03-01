using RestaurantAppData.DataContext;
using RestaurantAppData.DataContracts;
using RestaurantAppData.DbModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantAppData.İmplementation
{
    public class OrderItemRepository : Repository<OrderItem>, IOrderItemRepository
    {
        private readonly RestaurantAppDbContext _db;
        public OrderItemRepository(RestaurantAppDbContext db) : base(db)
        {
            _db = db;
        }
    }
}
