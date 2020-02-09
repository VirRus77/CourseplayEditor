using System;
using Pfim;
using SkiaSharp;

namespace CourseEditor.Drawing.Tools
{
    /// <summary>
    /// https://github.com/nickbabcock/Pfim/blob/master/src/Pfim.Skia/Program.cs
    /// </summary>
    public static class DdsHelper
    {
        public static SKImage Load(string filePath)
        {
            using var image = Pfim.Pfim.FromFile(filePath);
            var newData = image.Data;
            var stride = image.Stride;

            var colorType = SkColorType(image, ref newData, ref stride);
            var imageInfo = new SKImageInfo(image.Width, image.Height, colorType);
            using var stream = new SKMemoryStream(newData);
            using var data = SKData.Create(stream);
            var fromPixelData = SKImage.FromPixelData(imageInfo, data, stride);
            return fromPixelData;
        }

        private static SKColorType SkColorType(IImage image, ref byte[] newData, ref int stride)
        {
            SKColorType colorType;
            switch (image.Format)
            {
                case ImageFormat.Rgb8:
                    colorType = SKColorType.Gray8;
                    break;
                case ImageFormat.R5g6b5:
                    // color channels still need to be swapped
                    colorType = SKColorType.Rgb565;
                    break;
                case ImageFormat.Rgba16:
                    // color channels still need to be swapped
                    colorType = SKColorType.Argb4444;
                    break;
                case ImageFormat.Rgb24:
                    // Skia has no 24bit pixels, so we upscale to 32bit
                    var pixels = image.DataLen / 3;
                    var newDataLen = pixels * 4;
                    newData = new byte[newDataLen];
                    for (int i = 0; i < pixels; i++)
                    {
                        newData[i * 4] = image.Data[i * 3];
                        newData[i * 4 + 1] = image.Data[i * 3 + 1];
                        newData[i * 4 + 2] = image.Data[i * 3 + 2];
                        newData[i * 4 + 3] = 255;
                    }

                    stride = image.Width * 4;
                    colorType = SKColorType.Bgra8888;
                    break;
                case ImageFormat.Rgba32:
                    colorType = SKColorType.Bgra8888;
                    break;
                default:
                    throw new ArgumentException($"Skia unable to interpret pfim format: {image.Format}");
            }

            return colorType;
        }

    }
}
