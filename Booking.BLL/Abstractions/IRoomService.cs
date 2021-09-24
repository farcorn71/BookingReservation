using Booking.Core.Entities.ClientEntities.Request;
using Booking.Core.Entities.ClientEntities.Response;
using System;
using System.Collections.Generic;

namespace Booking.BLL.Business.Abstractions
{
    public interface IRoomService
    {
        bool Add(AddRoomRequest request);

        GetRoomResponse Get(Guid request);

        GetRoomResponse GetByRoomNo(string request);

        List<GetRoomResponse> GetAll();

        bool Delete(Guid request);
    }
}
