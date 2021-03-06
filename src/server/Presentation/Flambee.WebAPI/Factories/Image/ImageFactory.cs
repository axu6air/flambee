using AutoMapper;
using Flambee.Core.Domain.Image;
using Flambee.WebAPI.Models.Authentication;
using Flambee.WebAPI.Models.Avatar;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Flambee.WebAPI.Factories.Image
{
    public class ImageFactory : IImageFactory
    {
        private readonly IMapper _mapper;

        public ImageFactory(IMapper mapper)
        {
            _mapper = mapper;
        }


        public AvatarModel PrepareAvatarModel(AvatarUploadModel model, string filePath, int height = 0, int width = 0)
        {
            var avatarModel = new AvatarModel
            {
                UserId = model.UserId,
                AvatarBase64 = model.AvatarBase64,
                PreviewBase64 = model.PreviewBase64,
                DisplayName = model.AvatarImage.FileName,
                DefaultHeight = height,
                DefaultWidth = width,
                IsDeleted = false,
                MimeType = model.AvatarImage.ContentType,
                UploadTime = DateTime.Now,
                ModifiedTime = DateTime.Now,
                Title = model.Title,
                Url = filePath,
            };

            return avatarModel;
        }

        public Avatar PrepareAvatarEntityModel(AvatarModel model)
        {
            return _mapper.Map<Avatar>(model);
        }


    }
}
