using Booking.Core.Data.Repository.Abstractions;
using Booking.Core.Entities.BusinessEntities;
using System;

namespace Booking.Core.Data.Repository.Implementations
{
    public class RoomRepository : BaseRepository<Room>, IRoomRepository
    {
        public RoomRepository(IMongoDBContext context) : base(context)
        {

        }
    }
}
