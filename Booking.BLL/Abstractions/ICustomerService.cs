using Booking.Core.Entities.ClientEntities.Request;
using Booking.Core.Entities.ClientEntities.Response;
using System;
using System.Collections.Generic;

namespace Booking.BLL.Business.Abstractions
{
    public interface ICustomerService
    {
        bool Add(AddCustomerRequest request);

        GetCustomerResponse Get(Guid request);
        GetCustomerResponse Get(string requestId);

        List<GetCustomerResponse> GetAll();

        bool Delete(Guid request);
    }
}
