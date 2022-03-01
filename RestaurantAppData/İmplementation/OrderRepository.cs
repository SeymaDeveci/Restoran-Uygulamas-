using RestaurantAppData.DataContext;
using RestaurantAppData.DataContracts;
using RestaurantAppData.DbModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantAppData.İmplementation
{
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        private readonly RestaurantAppDbContext _db;
        public OrderRepository(RestaurantAppDbContext db) : base(db)
        {
            _db = db;
        }
    }
}
