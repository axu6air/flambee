using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Flambee.WebAPI.Models.Authentication
{
    public class AvatarUploadModel
    {
        public Guid UserId { get; set; }
        public string Title { get; set; }
        public string AvatarBase64 { get; set; }
        public string PreviewBase64 { get; set; }
        public IFormFile AvatarImage { get; set; }
    }
}
