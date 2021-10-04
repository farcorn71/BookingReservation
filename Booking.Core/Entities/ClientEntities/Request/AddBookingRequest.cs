using System;

namespace Booking.Core.Entities.ClientEntities.Request
{
    public class AddBookingRequest
    {
        public string CustomerId { get; set; }
        public string CustomerName { get; set; }

        public string RoomNo { get; set; }

        public decimal Amount { get; set; }
        public int NoOfdays { get; set; }

        public bool RoomStatus { get; set; }

        public string ActionBy { get; set; }
        public string ActionPerformed { get; set; }

    }
}
