using Booking.Core.Entities.ClientEntities.Request;
using Booking.Core.Entities.ClientEntities.Response;
using System;
using System.Collections.Generic;

namespace Booking.BLL.Business.Abstractions
{
    public interface IBookingReservationService
    {
        bool Add(AddBookingRequest request);

        GetBookingResponse Get(Guid request);

        GetBookingResponse GetByCustomerId(Guid request);

        GetBookingResponse GetByRoomNo(string roomNo);

        List<GetBookingResponse> GetAll();

        List<GetBookingResponse> GetCheckedIn();

        List<GetBookingResponse> GetCheckedOut();

        bool Delete(Guid request);
    }
}
