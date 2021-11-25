using Flambee.Core.Domain.Image;
using Flambee.Core.Domain.UserDetails;
using Microsoft.AspNetCore.Http;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flambee.Service.AppServiceProviders.Image
{
    public interface IImageService
    {
        Task<Avatar> AddAvatar(User user,Avatar avatar);

        string CheckImagePath(string path);

        string GetAvatarPath(string imageName);

        string GetPostImagePath(string imageName);

        string GetUniqueImageName(string imageName);

        bool UploadImage(IFormFile image, string filePath);

        Task<Avatar> GetAvatar(ObjectId userId);
    }
}
