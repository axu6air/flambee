using Flambee.Core.Configuration.Image;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flambee.Core.Domain.PostDetails
{
    public class PostImage: BaseImageEntity
    {
        public override string Directory => ImagePath.ImageDirectoryHierarchy;
    }
}
