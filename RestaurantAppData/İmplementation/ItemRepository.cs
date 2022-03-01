using RestaurantAppData.DataContext;
using RestaurantAppData.DataContracts;
using RestaurantAppData.DbModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantAppData.İmplementation
{
    public class ItemRepository : Repository<Items>, IItemRepository
    {
        private readonly RestaurantAppDbContext _db;
        public ItemRepository(RestaurantAppDbContext db) 
            : base(db)
        {
            _db = db;
        }
    }
}
