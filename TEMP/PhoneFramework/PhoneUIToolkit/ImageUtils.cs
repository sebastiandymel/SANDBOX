using System;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace SEDY.PhoneUIToolkit
{
    public static class ImageUtils
    {
        public static ImageSource GetImageSource(string fileName)
        {
            return new BitmapImage(new Uri(fileName, UriKind.Absolute));
        }
    }
}
