using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flambee.Core.Configuration.Image
{
    public class ImagePath
    {
        private const string AvatarFolderName = "Avatars";
        private const string PostImageFolderName = "PostImages";

        public static string AvatarPath => GetAvatarPath();
        public static string PostImagePath => GetPostImagePath();

        private static string GetAvatarPath()
        {
            return Path.DirectorySeparatorChar.ToString()
                                + "Images"
                                + Path.DirectorySeparatorChar.ToString()
                                + AvatarFolderName
                                + Path.DirectorySeparatorChar.ToString();
        }

        private static string GetPostImagePath()
        {
            return Path.DirectorySeparatorChar.ToString()
                                + "Images"
                                + Path.DirectorySeparatorChar.ToString()
                                + PostImageFolderName
                                + Path.DirectorySeparatorChar.ToString();
        }
    }
}
