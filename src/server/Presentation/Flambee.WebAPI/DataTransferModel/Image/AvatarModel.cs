using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Flambee.WebAPI.DataTransferModel.Image
{
    public class AvatarModel
    {
        public int Id { get; set; }
        public string AvatarBase64 { get; set; }
        public string PreviewBase64 { get; set; }
        public string Title { get; set; }
    }
}
