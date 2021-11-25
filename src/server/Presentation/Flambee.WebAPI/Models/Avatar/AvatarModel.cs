using Flambee.WebAPI.Models.User;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Flambee.WebAPI.Models.Avatar
{
    public class AvatarModel : BaseModel
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

        public ObjectId UserId { get; set; }

        public UserModel UserModel { get; set; }
    }
}
