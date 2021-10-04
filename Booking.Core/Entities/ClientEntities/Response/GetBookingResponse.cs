using System;

namespace Booking.Core.Entities.ClientEntities.Response
{
    public class GetBookingResponse
    {
        public Guid Id { get; set; }
        public string CustomerId { get; set; }
        public string CustomerName { get; set; }

        public string RoomNo { get; set; }

        public decimal Amount { get; set; }
        public int NoOfdays { get; set; }

        public bool RoomStatus { get; set; }
        public string ActionBy { get; set; }
        public string ActionPerformed { get; set; }

        public string ActionDate { get; set; }
    }
}
