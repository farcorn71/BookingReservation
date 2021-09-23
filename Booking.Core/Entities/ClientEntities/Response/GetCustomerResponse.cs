using System;

namespace Booking.Core.Entities.ClientEntities.Response
{
    public class GetCustomerResponse
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime CheckInDate { get; set; }

        public string EmailAddress { get; set; }

        public string PhoneNo { get; set; }
    }
}
