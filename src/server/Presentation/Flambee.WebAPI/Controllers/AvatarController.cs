using Flambee.Core.Domain.Image;
using Flambee.Service.AppServiceProviders.Image;
using Flambee.WebAPI.DataTransferModel.Image;
using Flambee.WebAPI.Factories.Image;
using Flambee.WebAPI.Models.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Flambee.WebAPI.Controllers
{
    public class AvatarController : BaseImageController
    {
        private readonly IImageService _imageService;
        private readonly IImageFactory _imageFactory;
        private readonly IHostEnvironment _environment;


        public AvatarController(IImageService imageService, IImageFactory imageFactory, IHostEnvironment environment)
        {
            _imageService = imageService;
            _imageFactory = imageFactory;
            _environment = environment;
        }


        [HttpPost, DisableRequestSizeLimit]
        [Route("/UploadAvatar")]
        public async Task<IActionResult> UploadAvatar([FromForm] AvatarUploadModel model)
        {
            if (model.AvatarImage != null)
            {
                try
                {
                    var newFilename = _imageService.GetUniqueImageName(model.AvatarImage.FileName);
                    var filePath = _imageService.GetAvatarPath(newFilename);
                    var result = _imageService.UploadImage(model.AvatarImage, filePath);

                    if (result)
                    {
                        (int height, int width) = GetImageDimension(model.AvatarImage);
                        var avatarModel = _imageFactory.PrepareAvatarModel(model, filePath, height, width);
                        var avatar = _imageFactory.PrepareAvatarEntityModel(avatarModel);
                        avatar = await _imageService.AddAvatar(avatar);
                        return Ok(avatar);
                    }
                }
                catch (Exception ex)
                {
                    return Problem();
                }
            }

            return BadRequest();
        }

        //[Authorize]
        [HttpGet]
        [Route("/GetAvatar")]
        public async Task<IActionResult> GetUserAvatar(Guid userId)
        {
            //Guid.TryParse(userId, out Guid userIdGuid);
            var avatar = await _imageService.GetAvatar(userId);

            var avatarModel = new AvatarModel();

            if (avatar != null)
            {
                avatarModel.AvatarBase64 = avatar.AvatarBase64;
                avatarModel.PreviewBase64 = avatar.PreviewBase64;
                avatarModel.Title = avatar.Title;
                avatarModel.Id = avatar.Id;
            }

            return Ok(avatarModel);
        }
    }
}
