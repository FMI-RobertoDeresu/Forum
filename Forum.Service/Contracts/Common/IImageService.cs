using System.Drawing;

namespace Forum.Service.Contracts.Common
{
    public interface IImageService
    {
        string JpegToBase64(Image image);
        Image Resize(Image image, int width, int height);
    }
}
