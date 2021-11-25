using Flambee.Core.Domain.Authentication;
using Flambee.Core.Domain.UserDetails;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Flambee.Core.Domain.Image
{
    public class Avatar : BaseEntity
    {

        public string AvatarBase64 { get; set; }
        public string PreviewBase64 { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        public string MimeType { get; set; }
        public double DefaultHeight { get; set; }
        public double DefaultWidth { get; set; }
        public DateTime UploadTime { get; set; }
        public DateTime ModifiedTime { get; set; }
        public string DisplayName { get; set; }
        public bool IsDeleted { get; set; }

        [BsonIgnore]
        public ObjectId UserId { get; set; }
    }

}
