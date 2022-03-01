using RestaurantAppData.DataContext;
using RestaurantAppData.DataContracts;

namespace RestaurantAppData.İmplementation
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly RestaurantAppDbContext _db;
        public UnitOfWork(RestaurantAppDbContext db)
        {
            _db = db;
            items = new ItemRepository(_db);
            customer = new CustomerRepository(_db);
            order = new OrderRepository(_db);
            orderitem = new OrderItemRepository(_db);
        }
        public IItemRepository items { get; private set; }
        public ICustomerRepository customer { get; private set; }
        public IOrderRepository order { get; private set; }
        public IOrderItemRepository orderitem { get; private set; }
        public void Dispose()
        {
            _db.Dispose();
        }

        public void Save() //Tüm aksiyonlar UnitOfWork katmanı üzerindeki SaveChanges üzerinden türeyecektir
        {
            _db.SaveChanges();
        }
    }
}
