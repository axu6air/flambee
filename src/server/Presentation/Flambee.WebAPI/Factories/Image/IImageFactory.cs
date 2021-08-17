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
        AvatarModel PrepareAvatarModel(AvatarUploadModel model, string filePath, int height = 0, int width = 0);
        Avatar PrepareAvatarEntityModel(AvatarModel model);

    }
}
