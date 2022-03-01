using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantAppData.DataContracts
{
    public interface IUnitOfWork : IDisposable
    {
        IItemRepository items { get; }
        ICustomerRepository customer { get; }
        IOrderRepository order { get; }
        IOrderItemRepository orderitem { get; }
        void Save();
    }
}
