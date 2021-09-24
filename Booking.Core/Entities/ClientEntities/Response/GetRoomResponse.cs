using System;

namespace Booking.Core.Entities.ClientEntities.Response
{
    public class GetRoomResponse
    {
        public Guid Id { get; set; }
        public string RoomName { get; set; }
        public string RoomNo { get; set; }
        public decimal Price { get; set; }

        public string ServicesDescription { get; set; }
    }
}
