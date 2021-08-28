using Flambee.Core.Domain.Image;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flambee.Service.AppServiceProviders.Image
{
    public interface IImageService
    {
        Task<Avatar> AddAvatar(Avatar avatar);

        string CheckImagePath(string path);

        string GetAvatarPath(string filename);

        string GetPostImagePath(string filename);

        string GetUniqueFileName(string fileName);

        bool UploadImage(IFormFile image, string filePath);
    }
}
