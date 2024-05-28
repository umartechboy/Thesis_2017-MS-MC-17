using SkiaSharp;

namespace Gregor_WASM.Services
{
    public class ImageServices
    {
        public static string SKImageToString(SKImage sKImage)
        {
            if (sKImage == null)
                return "";
            return "data:image/png;base64," + Convert.ToBase64String(sKImage.Encode(SKEncodedImageFormat.Png, 100).ToArray());
        }
        public static string SKImageToString(SKBitmap sKImage)
        {
            if (sKImage == null)
                return "";
            return "data:image/png;base64," + Convert.ToBase64String(sKImage.Encode(SKEncodedImageFormat.Png, 100).ToArray());
        }
    }
}
