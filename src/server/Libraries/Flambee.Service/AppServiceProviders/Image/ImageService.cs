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
using Flambee.Data;
using MongoDB.Driver.Linq;
using MongoDB.Bson;
using MongoDB.Driver;
using Flambee.Core.Domain.UserDetails;
using Flambee.Core.Domain.PostDetails;

namespace Flambee.Service.AppServiceProviders.Image
{
    public class ImageService : IImageService
    {
        private readonly IMongoRepository<User> _userRepository;
        private readonly IWebHostEnvironment _environment;

        public ImageService(IWebHostEnvironment environment, IMongoRepository<User> userRepository)
        {
            _userRepository = userRepository;
            _environment = environment;
        }

        public async Task<Avatar> AddAvatar(User user, Avatar avatar)
        {
            if (avatar != null && user != null)
            {
                user.Avatars.Add(avatar);
                await _userRepository.Update(user);
                return avatar;
            }

            return null;
        }

        public string CheckImagePath(string path)
        {
            if (!Directory.Exists(path))
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
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<Avatar> GetAvatar(ObjectId userId)
        {
            var user = await _userRepository.Query.Where(x=>x.Id.Equals(userId)).FirstOrDefaultAsync();
            if (user == null) 
                return null;
            var avatar = user.Avatars.OrderByDescending(x => x.UploadTime).FirstOrDefault();

            return avatar != null && !avatar.IsDeleted ? avatar : null;
        }
    }
}
