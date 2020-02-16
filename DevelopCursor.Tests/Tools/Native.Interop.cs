using System;
using System.Drawing;
using System.Runtime.InteropServices;

namespace DevelopCursor.Tests.Tools
{
    public static partial class Native
    {
        internal const int CursorShowing = 0x00000001;

        public class IconInfoSafe : IDisposable
        {
            private readonly IconInfo _iconInfo;

            internal IconInfoSafe(IconInfo iconInfo)
            {
                _iconInfo = iconInfo;
            }

            public IntPtr hbmMask => _iconInfo.hbmMask;
            public IntPtr hbmColor => _iconInfo.hbmColor;

            public void Dispose()
            {
                if (_iconInfo.hbmColor != IntPtr.Zero)
                {
                    DeleteObject(_iconInfo.hbmColor);
                }
                if (_iconInfo.hbmMask != IntPtr.Zero)
                {
                    DeleteObject(_iconInfo.hbmMask);
                }
                
            }
        }

        public static IconInfoSafe GetIconInfo(IntPtr hIcon) =>
            !GetIconInfo(hIcon, out var iconInfo)
                ? null
                : new IconInfoSafe(iconInfo);

        public static Bitmap CaptureImageCursor(ref Point point)
        {
            try
            {
                var cursorInfo = new CursorInfo();
                cursorInfo.cbSize = Marshal.SizeOf(cursorInfo);

                if (!GetCursorInfo(out cursorInfo))
                    return null;

                if (cursorInfo.flags != CursorShowing)
                    return null;

                var hicon = CopyIcon(cursorInfo.hCursor);
                if (hicon == IntPtr.Zero)
                    return null;

                IconInfo iconInfo;
                if (!GetIconInfo(hicon, out iconInfo))
                {
                    DestroyIcon(hicon);
                    return null;
                }

                point.X = cursorInfo.ptScreenPos.X - iconInfo.xHotspot;
                point.Y = cursorInfo.ptScreenPos.Y - iconInfo.yHotspot;

                using (var maskBitmap = Image.FromHbitmap(iconInfo.hbmMask))
                {
                    //Is this a monochrome cursor?  
                    if (maskBitmap.Height == maskBitmap.Width * 2 && iconInfo.hbmColor == IntPtr.Zero)
                    {
                        var final = new Bitmap(maskBitmap.Width, maskBitmap.Width);
                        var hDesktop = GetDesktopWindow();
                        var dcDesktop = GetWindowDC(hDesktop);

                        using (var resultGraphics = Graphics.FromImage(final))
                        {
                            var resultHdc = resultGraphics.GetHdc();

                            BitBlt(
                                resultHdc,
                                0,
                                0,
                                final.Width,
                                final.Height,
                                dcDesktop,
                                (int)point.X + 3,
                                (int)point.Y + 3,
                                CopyPixelOperation.SourceCopy
                            );
                            DrawIconEx(resultHdc, 0, 0, cursorInfo.hCursor, 0, 0, 0, IntPtr.Zero, 0x0003);

                            //TODO: I have to try removing the background of this cursor capture.
                            //Native.BitBlt(resultHdc, 0, 0, final.Width, final.Height, dcDesktop, (int)point.X + 3, (int)point.Y + 3, Native.CopyPixelOperation.SourceErase);

                            resultGraphics.ReleaseHdc(resultHdc);
                            ReleaseDC(hDesktop, dcDesktop);
                        }

                        DeleteObject(iconInfo.hbmMask);
                        DeleteDC(dcDesktop);
                        DestroyIcon(hicon);

                        return final;
                    }

                    DeleteObject(iconInfo.hbmColor);
                    DeleteObject(iconInfo.hbmMask);
                    DestroyIcon(hicon);
                }

                var icon = Icon.FromHandle(hicon);
                return icon.ToBitmap();
            }
            catch (Exception ex)
            {
                //You should catch exception with your method here.
                //LogWriter.Log(ex, "Impossible to get the cursor.");
            }

            return null;
        }
    }
}
