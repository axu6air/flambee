using Flambee.Core.Domain.Image;
using Flambee.Service.AppServiceProviders;
using Flambee.Service.AppServiceProviders.Image;
using Flambee.WebAPI.DataTransferModel.Image;
using Flambee.WebAPI.Factories.Image;
using Flambee.WebAPI.Models.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using MongoDB.Bson;
using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Flambee.WebAPI.Controllers
{
    [Authorize]
    public class AvatarController : BaseImageController
    {
        private readonly IImageService _imageService;
        private readonly IImageFactory _imageFactory;
        private readonly IHostEnvironment _environment;
        private readonly IUserService _userService;


        public AvatarController(IImageService imageService, IImageFactory imageFactory, IHostEnvironment environment, IUserService userService)
        {
            _imageService = imageService;
            _imageFactory = imageFactory;
            _environment = environment;
            _userService = userService;
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
                        var user = await _userService.FindById(model.UserId);
                        avatar = await _imageService.AddAvatar(user, avatar);
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

        [HttpGet]
        [Route("/GetAvatar")]
        public async Task<IActionResult> GetUserAvatar()
        {
            var userId = UserLoginInfo.UserId;
            var avatar = await _imageService.GetAvatar(userId);


            if (avatar != null)
                return Ok(new AvatarModel
                {
                    AvatarBase64 = avatar.AvatarBase64,
                    PreviewBase64 = avatar.PreviewBase64,
                    Title = avatar.Title,
                    Id = avatar.Id

                });

            return BadRequest();
        }
    }
}
