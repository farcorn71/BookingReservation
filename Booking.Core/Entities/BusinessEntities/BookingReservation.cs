using Booking.Core.Data;
using Booking.Core.Entities.BusinessEntities.Base;
using System;

namespace Booking.Core.Entities.BusinessEntities
{
    [BsonCollection("booking")]
    public class BookingReservation : BaseEntity
    {
        public string CustomerId { get; set; }
       
        public string RoomNo { get; set; }

        public int NoOfdays { get; set; }

        public bool RoomStatus { get; set; }
    }
}
