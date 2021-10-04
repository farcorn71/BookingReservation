using Booking.Core.Data;
using Booking.Core.Entities.BusinessEntities.Base;
using System;

namespace Booking.Core.Entities.BusinessEntities
{
    [BsonCollection("room")]
    public class Room : BaseEntity
    {
        public string RoomName { get; set; }
        public decimal Price { get; set; }
        public string ServicesDescription { get; set; }
        public string RoomNo { get; set; }
    }
}
