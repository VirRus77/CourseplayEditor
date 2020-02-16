using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using DevelopCursor.Tests.Tools;
using DevelopCursor.Tests.Tools.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MoreLinq.Extensions;

namespace DevelopCursor.Tests
{
    [TestClass]
    public class UnitTest1
    {
        //[TestMethod]
        public void TestMethod1()
        {
            var properties = typeof(Cursors).GetProperties(BindingFlags.Public | BindingFlags.Static);
            //var cursor = Cursors.Arrow;
            properties.ForEach(
                v =>
                {
                    using var bitmap = GetImage((Cursor)v.GetValue(null));
                    bitmap?.Save($"{v.Name}.png", ImageFormat.Png);
                }
            );
        }

        //[TestMethod]
        public void TestMethodCropToImage()
        {
            var cropFiles = Directory.GetFiles(@"G:\NickProd\Cursors\Excel\", "*.crop", SearchOption.TopDirectoryOnly);
            foreach (var cropFile in cropFiles)
            {
                var header = new byte[14];
                using var stream = new MemoryStream(header);
                using var writer = new BinaryWriter(stream);
                writer.Write(new byte[] { 0x42, 0x4D });
                var size = (int)new FileInfo(cropFile).Length;
                size = size - 2 + header.Length;
                writer.Write(BitConverter.GetBytes(size));
                writer.Write(new byte[4]);
                var startData = GetStartData(cropFile);
                writer.Write(BitConverter.GetBytes(startData));

                WriteFile(cropFile, header);
            }
        }

        private void WriteFile(string cropFile, byte[] header)
        {
            using var outputStream = File.Create(
                Path.Combine(Path.GetDirectoryName(cropFile), Path.GetFileNameWithoutExtension(cropFile) + ".bmp")
            );
            using var outputWriter = new BinaryWriter(outputStream);

            using var stream = File.OpenRead(cropFile);
            stream.Seek(4, SeekOrigin.Begin);
            //using var reader = new BinaryReader(stream);

            outputStream.Write(header, 0, header.Length);
            stream.CopyTo(outputStream);
        }

        private int GetStartData(string cropFile)
        {
            using var stream = File.OpenRead(cropFile);
            stream.Seek(4, SeekOrigin.Begin);
            using var reader = new BinaryReader(stream);
            var header = reader.ByteToType<BITMAPINFOHEADER>();
            
            return ReadColors(reader, header) - 4;
        }

        private int ReadColors(BinaryReader reader, BITMAPINFOHEADER header)
        {
            if ((header.bitsPerPixel != 24) && (header.bitsPerPixel != 32))
            {
                if (header.numberOfColors > 0)
                {
                    reader.ReadBytes(header.numberOfColors * 4);
                    //RGBQUAD aColors[header.numberOfColors];
                }
                else
                {
                    reader.ReadBytes((1 << header.bitsPerPixel) * 4);
                    //RGBQUAD aColors[1 << header.biBitCount];
                }
            }

            return (int)reader.BaseStream.Position;
        }

        private static Bitmap GetImage(Cursor cursorInstance)
        {
            using var iconInfo = Native.GetIconInfo(cursorInstance.Handle);
            IntPtr hicon = Native.CopyIcon(cursorInstance.Handle);
            using (Bitmap maskBitmap = Bitmap.FromHbitmap(iconInfo.hbmMask))
            {
                // check for monochrome cursor
                if (maskBitmap.Height == maskBitmap.Width * 2)
                {
                    Bitmap cursor = new Bitmap(32, 32, PixelFormat.Format32bppArgb);
                    Color BLACK = Color.FromArgb(255, 0, 0, 0);       //cannot compare Color.Black because of different names
                    Color WHITE = Color.FromArgb(255, 255, 255, 255); //cannot compare Color.White because of different names
                    for (int y = 0; y < 32; y++)
                    {
                        for (int x = 0; x < 32; x++)
                        {
                            Color maskPixel = maskBitmap.GetPixel(x, y);
                            Color cursorPixel = maskBitmap.GetPixel(x, y + 32);
                            if (maskPixel == WHITE && cursorPixel == BLACK)
                            {
                                cursor.SetPixel(x, y, Color.Transparent);
                            }
                            else if (maskPixel == BLACK)
                            {
                                cursor.SetPixel(x, y, cursorPixel);
                            }
                            else
                            {
                                cursor.SetPixel(x, y, cursorPixel == BLACK ? WHITE : BLACK);
                            }
                        }
                    }

                    return cursor;
                }
            }

            using var icon = Icon.FromHandle(hicon);
            return icon.ToBitmap();
        }
    }
}
