using Flambee.Core.Configuration.Image;
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
    public interface IImageFactory
    {
        AvatarModel PrepareAvatarModelForUpload(AvatarUploadModel model, string filename, int height = 0, int width = 0);
        Avatar PrepareAvatarEntityModel(AvatarModel model);
        Task<string> UploadImage(IFormFile image, ImageType imageType);
    }
}
