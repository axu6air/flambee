using Flambee.Core.Domain.Image;
using Flambee.Service.AppServiceProviders.Image;
using Flambee.WebAPI.Factories.Image;
using Flambee.WebAPI.Models.Authentication;
using Microsoft.AspNetCore.Mvc;
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


        public AvatarController(IImageService imageService, IImageFactory imageFactory)
        {
            _imageService = imageService;
            _imageFactory = imageFactory;
        }


        [HttpPost, DisableRequestSizeLimit]
        [Route("/UploadAvatar")]
        public async Task<IActionResult> UploadAvatar([FromForm] AvatarUploadModel model)
        {
            if (model.AvatarImage != null)
            {
                try
                {
                    var newFilename = _imageService.GetUniqueFileName(model.AvatarImage.FileName);
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
    }
}
