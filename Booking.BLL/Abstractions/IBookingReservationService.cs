using Booking.Core.Entities.ClientEntities.Request;
using Booking.Core.Entities.ClientEntities.Response;
using System;
using System.Collections.Generic;

namespace Booking.BLL.Business.Abstractions
{
    public interface IBookingReservationService
    {
        bool Add(AddBookingRequest request);

        void Update(AddBookingRequest request);

        GetBookingResponse Get(Guid request);

        List<GetBookingResponse> GetByCustomerId(string request);
        GetBookingResponse GetByCustomerLastBookingId(string customerId);

        GetBookingResponse GetByRoomNoLastBooking(string roomNo);
        List<GetBookingResponse> GetByRoomNo(string roomNo);

        List<GetBookingResponse> GetAll();

        List<GetBookingResponse> GetCheckedIn();

        List<GetBookingResponse> GetCheckedOut();

        bool Delete(Guid request);
    }
}
