using Booking.Core.Data.Repository.Abstractions;
using Booking.Core.Entities.BusinessEntities;
using System;

namespace Booking.Core.Data.Repository.Implementations
{
    public class BookingReservationRepository : BaseRepository<BookingReservation>, IBookingReservationRepository
    {
        public BookingReservationRepository(IMongoDBContext context) : base(context)
        {

        }
    }
}
