﻿using System;

namespace Booking.Core.Entities.ClientEntities.Request
{
    public class AddBookingRequest
    {
        public string CustomerId { get; set; }

        public string RoomNo { get; set; }

        public int NoOfdays { get; set; }

        public bool RoomStatus { get; set; }
    }
}
