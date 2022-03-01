using RestaurantAppData.DataContext;
using RestaurantAppData.DataContracts;
using RestaurantAppData.DbModels;

namespace RestaurantAppData.İmplementation
{
    public class CustomerRepository : Repository<Customer>, ICustomerRepository
    {
        private readonly RestaurantAppDbContext _db;
        public CustomerRepository(RestaurantAppDbContext db) 
            : base(db)
        {
            _db = db;
        }
    }
}
