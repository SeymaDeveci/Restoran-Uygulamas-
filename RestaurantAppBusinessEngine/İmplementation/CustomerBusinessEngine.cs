using RestaurantAppBusinessEngine.Contracts;
using RestaurantAppCommon.Dtos;
using RestaurantAppCommon.ResultConstant;
using RestaurantAppData.DataContracts;
using System.Collections.Generic;
using System.Linq;

namespace RestaurantAppBusinessEngine.İmplementation
{
    public class CustomerBusinessEngine : ICustomerBusinessEngine
    {
        private readonly IUnitOfWork _uow;
        public CustomerBusinessEngine(IUnitOfWork uow)
        {
            _uow = uow;
        }
        public Result<List<CustomerDto>> GetCustomers()
        {
            List<CustomerDto> customer = new List<CustomerDto>();
            var data = _uow.customer.GetAll().ToList(); //Dataları getir
            if (data != null)
            {
                foreach (var item in data)
                {
                    customer.Add(new CustomerDto()
                    {
                        CustomerId = item.CustomerId,
                        Name = item.Name,
                    });
                }
                return new Result<List<CustomerDto>>(true, ResultConstant.RecordFound, customer);
            }
            return new Result<List<CustomerDto>>(false, ResultConstant.RecordNotFound);
        }
    }
}
