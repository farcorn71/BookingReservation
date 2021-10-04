using System;

namespace Booking.Core.Entities.ClientEntities.Request
{
    public class AddRoomRequest
    {
        public string RoomName { get; set; }
        public decimal Price { get; set; }
        public string ServicesDescription { get; set; }

        public string RoomNo { get; set; }
        public string ActionBy { get; set; }
        public string ActionPerformed { get; set; }
    }
}
