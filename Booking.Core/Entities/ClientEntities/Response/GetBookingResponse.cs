using System;

namespace Booking.Core.Entities.ClientEntities.Response
{
    public class GetBookingResponse
    {
        public Guid Id { get; set; }
        public string CustomerId { get; set; }

        public string RoomNo { get; set; }

        public int NoOfdays { get; set; }

        public bool RoomStatus { get; set; }
    }
}
