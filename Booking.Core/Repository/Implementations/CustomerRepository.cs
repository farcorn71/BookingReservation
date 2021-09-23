using Booking.Core.Data.Repository.Abstractions;
using Booking.Core.Entities.BusinessEntities;
using System;

namespace Booking.Core.Data.Repository.Implementations
{
    public class CustomerRepository : BaseRepository<Customer>, ICustomerRepository
    {
        public CustomerRepository(IMongoDBContext context) : base(context)
        {

        }
    }
}
