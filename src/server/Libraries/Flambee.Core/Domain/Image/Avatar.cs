using Flambee.Core.Domain.Authentication;
using Flambee.Core.Domain.UserDetails;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Flambee.Core.Domain.Image
{
    public class Avatar : BaseImageEntity
    {

        public string AvatarBase64 { get; set; }

        [BsonIgnore]
        public ObjectId UserId { get; set; }
    }

}
