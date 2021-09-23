using Booking.Core.Entities.ClientEntities.Request;
using Booking.Core.Entities.ClientEntities.Response;
using System;

namespace Booking.BLL.Business.Abstractions
{
    public interface ICustomerService
    {
        bool Add(AddCustomerRequest request);

        GetCustomerResponse Get(Guid request);

        bool Delete(Guid request);
    }
}
