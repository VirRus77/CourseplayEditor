using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace DevelopCursor.Tests.Tools
{
    public static partial class Native
    {
        private const string Gdi32 = "gdi32.dll";

        ///<summary>
        ///Performs a bit-block transfer of the color data corresponding to a
        ///rectangle of pixels from the specified source device context into
        ///a destination device context.
        ///</summary>
        ///<param name="hdc">Handle to the destination device context.</param>
        ///<param name="nXDest">The leftmost x-coordinate of the destination rectangle (in pixels).</param>
        ///<param name="nYDest">The topmost y-coordinate of the destination rectangle (in pixels).</param>
        ///<param name="nWidth">The width of the source and destination rectangles (in pixels).</param>
        ///<param name="nHeight">The height of the source and the destination rectangles (in pixels).</param>
        ///<param name="hdcSrc">Handle to the source device context.</param>
        ///<param name="nXSrc">The leftmost x-coordinate of the source rectangle (in pixels).</param>
        ///<param name="nYSrc">The topmost y-coordinate of the source rectangle (in pixels).</param>
        ///<param name="dwRop">A raster-operation code.</param>
        ///<returns>
        ///<c>true</c> if the operation succeedes, <c>false</c> otherwise. To get extended error information, call <see cref="System.Runtime.InteropServices.Marshal.GetLastWin32Error"/>.
        ///</returns>
        [DllImport(Gdi32, EntryPoint = "BitBlt", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool BitBlt([In] IntPtr hdc, int nXDest, int nYDest, int nWidth, int nHeight, [In] IntPtr hdcSrc, int nXSrc, int nYSrc, CopyPixelOperation dwRop);

        ///<summary>Deletes the specified device context (DC).</summary>
        ///<param name="hdc">A handle to the device context.</param>
        ///<returns><para>If the function succeeds, the return value is nonzero.</para><para>If the function fails, the return value is zero.</para></returns>
        ///<remarks>An application must not delete a DC whose handle was obtained by calling the <c>GetDC</c> function. Instead, it must call the <c>ReleaseDC</c> function to free the DC.</remarks>
        [DllImport(Gdi32, EntryPoint = "DeleteDC")]
        internal static extern bool DeleteDC([In] IntPtr hdc);

        ///<summary>Deletes a logical pen, brush, font, bitmap, region, or palette, freeing all system resources associated with the object. After the object is deleted, the specified handle is no longer valid.</summary>
        ///<param name="hObject">A handle to a logical pen, brush, font, bitmap, region, or palette.</param>
        ///<returns>
        ///<para>If the function succeeds, the return value is nonzero.</para>
        ///<para>If the specified handle is not valid or is currently selected into a DC, the return value is zero.</para>
        ///</returns>
        ///<remarks>
        ///<para>Do not delete a drawing object (pen or brush) while it is still selected into a DC.</para>
        ///<para>When a pattern brush is deleted, the bitmap associated with the brush is not deleted. The bitmap must be deleted independently.</para>
        ///</remarks>
        [DllImport(Gdi32, EntryPoint = "DeleteObject")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool DeleteObject([In] IntPtr hObject);
    }
}
