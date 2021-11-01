using System.IO;

namespace Flambee.Core.Configuration.Image
{
    public class ImagePath
    {
        private const string AvatarFolderName = "Avatars";
        private const string PostImageFolderName = "PostImages";

        public static string AvatarPath => Path.DirectorySeparatorChar.ToString()
                                + "Images"
                                + Path.DirectorySeparatorChar.ToString()
                                + AvatarFolderName
                                + Path.DirectorySeparatorChar.ToString();
        public static string PostImagePath => Path.DirectorySeparatorChar.ToString()
                                + "Images"
                                + Path.DirectorySeparatorChar.ToString()
                                + PostImageFolderName
                                + Path.DirectorySeparatorChar.ToString();

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
