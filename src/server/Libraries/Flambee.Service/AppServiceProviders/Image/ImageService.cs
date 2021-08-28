﻿using Flambee.Core;
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

        public string GetAvatarPath(string filename)
        {
            var path = CheckImagePath(ImagePath.AvatarPath);
            return path + filename;
        }

        public string GetPostImagePath(string filename)
        {
            var path = CheckImagePath(ImagePath.PostImagePath);
            return path + filename;
        }

        public string GetUniqueFileName(string fileName)
        {
            fileName = Path.GetFileName(fileName);
            return Path.GetFileNameWithoutExtension(fileName)
                      + "_"
                      + Guid.NewGuid().ToString().Substring(0, 4)
                      + Path.GetExtension(fileName);
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

        
    }
}
