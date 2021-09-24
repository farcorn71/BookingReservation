using System;

namespace Booking.Core.Entities.ClientEntities.Response
{
    public class GetDashBoardResponse
    {
        public int rooms { get; set; }

        public int customers { get; set; }

        public int checkins { get; set; }

        public int checkouts { get; set; }

    }
}
