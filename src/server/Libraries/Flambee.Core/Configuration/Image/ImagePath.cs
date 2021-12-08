using System.IO;
using System.Text;

namespace Flambee.Core.Configuration.Image
{
    public class ImagePath
    {
        private static readonly string[] avatarDirectoryFolders = { "Images", "Avatars" };
        private static readonly string[] postImageDirectoryFolders = { "Images", "PostImages" };

        public static string AvatarLocalDirectoryPath => GetAvatarUploadPath();
        public static string AvatarUrlPath => GetAvatarFetchPath();

        public static string PostImageLocalDirectoryPath => GetPostImageUploadPath();
        public static string PostImageUrlPath => GetPostImageFetchPath();

        public static string AvatarDirectoryHierarchy => GetPath(avatarDirectoryFolders, null);
        public static string ImageDirectoryHierarchy => GetPath(postImageDirectoryFolders, null);

        private static string GetAvatarUploadPath()
        {
            return GetPath(avatarDirectoryFolders, Path.DirectorySeparatorChar);
        }

        private static string GetAvatarFetchPath()
        {
            return GetPath(avatarDirectoryFolders, Path.AltDirectorySeparatorChar);
        }

        private static string GetPostImageUploadPath()
        {
            return GetPath(postImageDirectoryFolders, Path.DirectorySeparatorChar);
        }

        private static string GetPostImageFetchPath()
        {
            return GetPath(postImageDirectoryFolders, Path.AltDirectorySeparatorChar);
        }

        public static string GetPath(string[] source, char? separator)
        {
            if (source.Length == 0)
                return null;

            if (separator == null)
                separator = ',';

            StringBuilder path = new();
            if (separator != ',')
                path.Append(separator);

            for (int i = 0; i < source.Length; i++)
            {
                path.Append(source[i]);
                path.Append(separator);
            }

            return path.ToString();
        }
    }
}
