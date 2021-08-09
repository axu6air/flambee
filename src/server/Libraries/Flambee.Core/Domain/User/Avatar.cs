using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flambee.Core.Domain.User
{
    public class Avatar : BaseEntity
    {
        public byte[] AvatarBinary { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        public string MimeType { get; set; }
        public double DefaultHeight { get; set; }
        public double DefaultWidth { get; set; }
        public DateTime UploadTime { get; set; }
        public bool IsDeleted { get; set; }

        [ForeignKey("UserInfo")]
        public int UserId { get; set; }

        public UserInfo UserInfo { get; set; }
    }
}
