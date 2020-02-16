using System.Runtime.InteropServices;

namespace DevelopCursor.Tests.Tools
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct BITMAPINFOHEADER
    {
        // the size of this header, in bytes (40)
        public int sizeThisHeader;
        // the bitmap width in pixels (signed integer)
        public int width;
        // the bitmap height in pixels (signed integer)
        public int height;
        // the number of color planes (must be 1)
        public short number;
        // the number of bits per pixel, which is the color depth of the image. Typical values are 1, 4, 8, 16, 24 and 32.
        public short bitsPerPixel;
        // the compression method being used. See the next table for a list of possible values
        public int compression;
        // the image size. This is the size of the raw bitmap data; a dummy 0 can be given for BI_RGB bitmaps.
        public int imageSize;
        // the horizontal resolution of the image. (pixel per metre, signed integer)
        public int horizontalResolution;
        // the vertical resolution of the image. (pixel per metre, signed integer)
        public int verticalResolution;
        // the number of colors in the color palette, or 0 to default to 2n
        public int numberOfColors;
        // the number of important colors used, or 0 when every color is important; generally ignored
        public int importantColors;
    };

    public struct BColor
    {
        public byte Green;
        public byte Blue;
        public byte Red;
        public byte Reserved;
    }
}
