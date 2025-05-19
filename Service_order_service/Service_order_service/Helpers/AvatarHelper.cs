using System.IO;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Service_order_service.Helpers
{
    public class AvatarHelper
    {
        public static void LoadAvatarImage(Image imageControl, string? avatarPath)
        {
            if (!string.IsNullOrWhiteSpace(avatarPath) && File.Exists(avatarPath))
            {
                DisplayAvatar(imageControl, avatarPath);
            }
            else
            {
                imageControl.Source = null;
            }
        }

        public static void DisplayAvatar(Image imageControl, string path)
        {
            var bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri(path, UriKind.Absolute);
            bitmap.CacheOption = BitmapCacheOption.OnLoad;
            bitmap.EndInit();

            imageControl.Source = bitmap;
        }
    }
}
