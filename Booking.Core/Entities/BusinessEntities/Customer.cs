using Booking.Core.Data;
using Booking.Core.Entities.BusinessEntities.Base;
using System;

namespace Booking.Core.Entities.BusinessEntities
{
    [BsonCollection("customer")]
    public class Customer : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }

        public string PhoneNo { get; set; }
        public DateTime CheckoutDate { get; set; }
    }
}
