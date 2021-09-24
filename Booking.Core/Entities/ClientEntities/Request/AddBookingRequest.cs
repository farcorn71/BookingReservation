using System;

namespace Booking.Core.Entities.ClientEntities.Request
{
    public class AddBookingRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string EmailAddress { get; set; }

        public string PhoneNo { get; set; }
    }
}
