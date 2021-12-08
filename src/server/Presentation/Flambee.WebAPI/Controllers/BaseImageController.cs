using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Net.Http.Headers;
using Flambee.Core.Configuration.Image;
using System.IO;
using Microsoft.AspNetCore.Mvc;

namespace Flambee.WebAPI.Controllers
{
    public class BaseImageController : BaseController
    {
        protected (int height, int width) GetImageDimension(IFormFile file)
        {
            using var image = Image.FromStream(file.OpenReadStream());
            return (image.Height, image.Width);
        }

        protected string GetImageUrl(string filename, string[] directoryHierarchy)
        {
            if (string.IsNullOrEmpty(filename))
                return null;

            var httpContextAccessor = HttpContext.RequestServices.GetService<IHttpContextAccessor>();
            var hostHeader = httpContextAccessor.HttpContext.Request.Headers[HeaderNames.Host].ToString();
            ImagePath.GetPath(directoryHierarchy, Path.AltDirectorySeparatorChar);
            return $"{this.Request.Scheme}://" + hostHeader + ImagePath.AvatarUrlPath + filename;
        }
    }
}
