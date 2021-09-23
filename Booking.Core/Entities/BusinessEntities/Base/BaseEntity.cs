using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace Booking.Core.Entities.BusinessEntities.Base
{
    public class BaseEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        public Guid Id { get; set; }

        public DateTime CreateDate { get; set; }
    }
}
