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

        public string ActionBy { get; set; }
        public string ActionPerformed { get; set; }

        public DateTime CreateDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
