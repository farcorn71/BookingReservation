using System;
using Microsoft.Extensions.DependencyInjection;
using Booking.BLL.Business.Abstractions;
using Booking.Core.Data.Repository.Abstractions;
using Booking.Core.Entities.BusinessEntities;
using Booking.Core.Entities.ClientEntities;
using Booking.Core.Entities.ClientEntities.Request;
using Booking.Core.Entities.ClientEntities.Response;

namespace Booking.BLL.Business.Implementations
{
    public class CustomerService : ICustomerService
    {
        private readonly IServiceProvider _collection;

        public CustomerService(IServiceProvider collection)
        {
            _collection = collection;
        }

        public bool Add(AddCustomerRequest request)
        {
            Customer entity = new Customer();
            //entity.BirthDate = request.BirthDate;
            entity.FirstName = request.FirstName;
            entity.LastName = request.LastName;
            entity.CreateDate = DateTime.Now;

            var customer = _collection.GetService<ICustomerRepository>().Add(entity);

            return customer != null ? true : false;
        }

        public GetCustomerResponse Get(Guid request)
        {
            GetCustomerResponse response = new GetCustomerResponse();

            var customer = _collection.GetService<ICustomerRepository>().GetById(request);

            if (customer != null)
            {
                response.Id = customer.Id;
                response.FirstName = customer.FirstName;
                response.LastName = customer.LastName;
                //response.BirthDate = customer.BirthDate;
            }
            return response;
        }

        public bool Delete(Guid request)
        {
            _collection.GetService<ICustomerRepository>().Remove(request);
            return true;
        }
    }
}
