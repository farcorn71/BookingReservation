using System;
using Microsoft.Extensions.DependencyInjection;
using Booking.BLL.Business.Abstractions;
using Booking.Core.Data.Repository.Abstractions;
using Booking.Core.Entities.BusinessEntities;
using Booking.Core.Entities.ClientEntities;
using Booking.Core.Entities.ClientEntities.Request;
using Booking.Core.Entities.ClientEntities.Response;
using System.Collections.Generic;
using System.Linq;

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
            entity.EmailAddress = request.EmailAddress;
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
                response.EmailAddress = customer.EmailAddress;
                //response.BirthDate = customer.BirthDate;
            }
            return response;
        }

        public List<GetCustomerResponse> GetAll()
        {
            List <GetCustomerResponse> responses = new List<GetCustomerResponse>();

            IEnumerable<Customer> customers = _collection.GetService<ICustomerRepository>().GetAll().AsEnumerable();

            foreach(var customer in customers)
            {
                GetCustomerResponse response = new GetCustomerResponse();
                response.Id = customer.Id;
                response.FirstName = customer.FirstName;
                response.EmailAddress = customer.EmailAddress;
                response.LastName = customer.LastName;
                responses.Add(response);
                //response.BirthDate = customer.BirthDate;
            }
            return responses;
        }

        public bool Delete(Guid request)
        {
            _collection.GetService<ICustomerRepository>().Remove(request);
            return true;
        }
    }
}
