using Microsoft.AspNetCore.Http;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Flambee.WebAPI.Models.Authentication
{
    public class AvatarUploadModel
    {
        [Required]
        public ObjectId UserId { get; set; }

        public string Title { get; set; }
        [Required]
        public string AvatarBase64 { get; set; }
        [Required]
        public string PreviewBase64 { get; set; }
        [Required]
        public IFormFile AvatarImage { get; set; }
    }
}
