using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace Flambee.WebAPI.Controllers
{
    public class BaseImageController : BaseController
    {
        protected (int height, int width) GetImageDimension(IFormFile file)
        {
            using var image = Image.FromStream(file.OpenReadStream());
            return (image.Height, image.Width);
        }
    }
}
