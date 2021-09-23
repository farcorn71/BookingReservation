using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Booking.Core.Entities.ClientEntities.Request;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Booking.BLL.Business.Abstractions;
using Booking.Core.Entities.ClientEntities.Response;

namespace BookingApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomerController : ControllerBase
    {
       

        private readonly ILogger<CustomerController> _logger;

        public CustomerController(ILogger<CustomerController> logger)
        {
            _logger = logger;
        }

        private readonly IServiceProvider _collection;

        public CustomerController(IServiceProvider collection)
        {
            _collection = collection;
        }

        [HttpPost]
        [Route("Add")]
        public bool Add(AddCustomerRequest request)
        {
            using (var scope = _collection.CreateScope())
            {
                return _collection.GetService<ICustomerService>().Add(request);
            }
        }

        [HttpGet]
        [Route("Get")]
        public GetCustomerResponse Get(Guid q)
        {
            using (var scope = _collection.CreateScope())
            {
                return _collection.GetService<ICustomerService>().Get(q);
            }
        }
    }
}
