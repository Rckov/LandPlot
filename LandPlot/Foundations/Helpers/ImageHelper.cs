using System.IO;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace LandPlot.Foundations.Helpers;

internal static class ImageHelper
{
    public static void SaveImageToFile(Image image, string filePath)
    {
        var bitmapSource = (BitmapSource)image.Source;

        using var fileStream = new FileStream(filePath, FileMode.Create);

        var encoder = new PngBitmapEncoder();
        encoder.Frames.Add(BitmapFrame.Create(bitmapSource));
        encoder.Save(fileStream);
    }
}