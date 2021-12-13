using Flambee.Core.Configuration.Image;
using Flambee.Core.Domain.Image;
using Flambee.Service.AppServiceProviders;
using Flambee.Service.AppServiceProviders.Image;
using Flambee.WebAPI.DataTransferModel.Image;
using Flambee.WebAPI.Factories.Image;
using Flambee.WebAPI.Models.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Microsoft.Net.Http.Headers;
using MongoDB.Bson;
using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Flambee.WebAPI.Controllers
{

    public class AvatarController : BaseImageController
    {
        private readonly IImageService _imageService;
        private readonly IImageFactory _imageFactory;
        private readonly IHostEnvironment _environment;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IUserService _userService;
        private readonly IHttpContextAccessor _httpContextAccessor;


        public AvatarController(IImageService imageService, IImageFactory imageFactory, IHostEnvironment environment, IUserService userService, IWebHostEnvironment webHostEnvironment, IHttpContextAccessor httpContextAccessor)
        {
            _imageService = imageService;
            _imageFactory = imageFactory;
            _environment = environment;
            _userService = userService;
            _webHostEnvironment = webHostEnvironment;
            _httpContextAccessor = httpContextAccessor;
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
                        var avatarModel = _imageFactory.PrepareAvatarModelForUpload(model, newFilename, height, width);
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
        [Authorize]
        public async Task<IActionResult> GetUserAvatar()
        {
            var userId = LoggedInUserModel.UserId;
            var avatar = await _imageService.GetAvatar(userId);
            string url = "";

            if (avatar != null)
            {
                url = GetImageUrl(avatar.FileName, avatar.DirectoryArray);

                return Ok(new AvatarModel
                {
                    AvatarBase64 = avatar.ImageBase64,
                    PreviewBase64 = avatar.PreviewBase64,
                    Title = avatar.Title,
                    Id = avatar.Id,
                    ImageUrl = url

                });
            }

            return BadRequest();
        }
    }
}
