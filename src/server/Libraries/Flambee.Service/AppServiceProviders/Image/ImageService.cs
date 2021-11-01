using Flambee.Core;
using Flambee.Core.Domain.Image;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Drawing;
using System.Threading.Tasks;
using Flambee.Core.Configuration.Image;
using Microsoft.EntityFrameworkCore;

namespace Flambee.Service.AppServiceProviders.Image
{
    public class ImageService : IImageService
    {
        private readonly IRepository<Avatar> _avatarRepository;
        private readonly IWebHostEnvironment _environment;

        public ImageService(IRepository<Avatar> avatarRepository, IWebHostEnvironment environment)
        {
            _avatarRepository = avatarRepository;
            _environment = environment;
        }

        public async Task<Avatar> AddAvatar(Avatar avatar)
        {
            if (avatar != null)
            {
                avatar = await _avatarRepository.InsertAsync(avatar);
                return avatar;
            }

            return null;
        }

        public string CheckImagePath(string path)
        {
            if(!Directory.Exists(path))
                Directory.CreateDirectory(path);

            return path;
        }

        public string GetAvatarPath(string imageName)
        {
            var path = CheckImagePath(ImagePath.AvatarPath);
            return path + imageName;
        }

        public string GetPostImagePath(string imageName)
        {
            var path = CheckImagePath(ImagePath.PostImagePath);
            return path + imageName;
        }

        public string GetUniqueImageName(string imageName)
        {
            imageName = Path.GetFileName(imageName);
            //return Path.GetFileNameWithoutExtension(imageName)
            //          + "_"
            //          + Guid.NewGuid().ToString().Substring(0, 4)
            //          + Path.GetExtension(imageName);
            return string.Concat(Path.GetFileNameWithoutExtension(imageName)
                                , "_"
                                , Guid.NewGuid().ToString().AsSpan(0, 4)
                                , Path.GetExtension(imageName));
        }

        public bool UploadImage(IFormFile image, string filePath)
        {
            try
            {
                image.CopyTo(new FileStream(_environment.WebRootPath + filePath, FileMode.Create));
                return true;
            } 
            catch(Exception ex)
            {
                return false;
            }
        }

        public async Task<Avatar> GetAvatar(Guid userId)
        {
            var avatar = await (from av in _avatarRepository.Table
                          where userId.Equals(av.UserId)
                          orderby av.Id descending
                          select av
                          ).FirstOrDefaultAsync();

            return avatar != null && !avatar.IsDeleted ? avatar : null;

        }


    }
}
