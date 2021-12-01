using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;
using System;

namespace Flambee.Core
{
    public interface IBaseEntity
    {
        public ObjectId Id { get; set; }
        public DateTime UploadTime { get; set; }
        public DateTime ModifiedTime { get; set; }
        public bool IsDeleted { get; set; }

    }

    public class BaseEntity : IBaseEntity
    {
        public BaseEntity()
        {
            Id = ObjectId.GenerateNewId();
            UploadTime = DateTime.Now;
            ModifiedTime = DateTime.Now;
            IsDeleted = false;
        }

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonIgnoreIfDefault]
        public ObjectId Id { get; set; }

        public DateTime UploadTime { get; set; }
        public DateTime ModifiedTime { get; set; }
        public bool IsDeleted { get; set; }
    }

    public class BaseImageEntity : BaseEntity
    {
        public string ImageBase64 { get; set; }
        public string PreviewBase64 { get; set; }
        public string Title { get; set; }
        public string VirtualPath { get; set; }
        public string MimeType { get; set; }
        public double DefaultHeight { get; set; }
        public double DefaultWidth { get; set; }
        public string DisplayName { get; set; }
    }
}
