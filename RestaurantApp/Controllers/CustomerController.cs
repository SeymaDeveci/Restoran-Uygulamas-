using Microsoft.AspNetCore.Mvc;
using RestaurantAppBusinessEngine.Contracts;
using RestaurantAppCommon.Dtos;
using System.Collections.Generic;

namespace RestaurantApp.Controllers
{
    [Route("api/customer")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerBusinessEngine _customerBusinessEngine;
        public CustomerController(ICustomerBusinessEngine customerBusinessEngine)
        {
            _customerBusinessEngine = customerBusinessEngine;
        }
        [HttpGet("GetCustomerList")]
        public List<CustomerDto> GetCustomerList()
        {
            return _customerBusinessEngine.GetCustomers().Data;
        }
    }
}
