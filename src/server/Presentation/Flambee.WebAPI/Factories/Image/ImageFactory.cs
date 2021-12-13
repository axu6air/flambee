using AutoMapper;
using Flambee.Core.Configuration.Image;
using Flambee.Core.Domain.Image;
using Flambee.Service.AppServiceProviders.Image;
using Flambee.WebAPI.Models.Authentication;
using Flambee.WebAPI.Models.Avatar;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Flambee.WebAPI.Factories.Image
{
    public class ImageFactory : IImageFactory
    {
        private readonly IMapper _mapper;
        private readonly IImageService _imageService;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ImageFactory(IMapper mapper, IImageService imageService, IWebHostEnvironment webHostEnvironment)
        {
            _mapper = mapper;
            _imageService = imageService;
            _webHostEnvironment = webHostEnvironment;

            

            
        }


        public AvatarModel PrepareAvatarModelForUpload(AvatarUploadModel model, string filename, int height = 0, int width = 0)
        {
            var avatarModel = new AvatarModel
            {
                ImageBase64 = model.AvatarBase64,
                PreviewBase64 = model.PreviewBase64,
                DisplayName = model.AvatarImage.FileName,
                DefaultHeight = height,
                DefaultWidth = width,
                MimeType = model.AvatarImage.ContentType,
                UploadTime = DateTime.Now,
                ModifiedTime = DateTime.Now,
                Title = model.Title,
                Directory = ImagePath.AvatarDirectoryHierarchy,
                FileName = filename,
            };

            return avatarModel;
        }

        public Avatar PrepareAvatarEntityModel(AvatarModel model)
        {
            return _mapper.Map<Avatar>(model);
        }

        private static string GetUniqueImageName(string imageName)
        {
            imageName = Path.GetFileName(imageName);

            return string.Concat(Path.GetFileNameWithoutExtension(imageName)
                                , "_"
                                , Guid.NewGuid().ToString().AsSpan(0, 8)
                                , Path.GetExtension(imageName));
        }

        public async Task<string> UploadImage(IFormFile image, ImageType imageType)
        {
            if (image == null)
                return string.Empty;

            string filePath = "";

            var uniqueFilename = GetUniqueImageName(image.FileName);
            if (imageType == ImageType.Avatar)
            {
                if (!Directory.Exists(ImagePath.AvatarLocalDirectoryPath))
                    Directory.CreateDirectory(ImagePath.AvatarLocalDirectoryPath);

                filePath = ImagePath.AvatarLocalDirectoryPath + uniqueFilename;
            }
            else if (imageType == ImageType.Post)
            {
                if (!Directory.Exists(ImagePath.PostImageLocalDirectoryPath))
                    Directory.CreateDirectory(ImagePath.PostImageLocalDirectoryPath);

                filePath = ImagePath.PostImageLocalDirectoryPath + uniqueFilename;
            }

            try
            {
                await image.CopyToAsync(new FileStream(_webHostEnvironment.WebRootPath + filePath, FileMode.Create));
                return uniqueFilename;
            }
            catch (Exception)
            {
            }

            return string.Empty;
        }
    }
}
