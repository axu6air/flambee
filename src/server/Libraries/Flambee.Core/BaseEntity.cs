using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;
using System;

namespace Flambee.Core
{
    public interface IBaseEntity
    {
        public ObjectId Id { get; set; }

    }

    public class BaseEntity : IBaseEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonIgnoreIfDefault]
        public ObjectId Id { get; set; }

        public BaseEntity()
        {
            this.Id = ObjectId.GenerateNewId();
        }
    }
}
