using System;
using System.Runtime.InteropServices;

namespace DevelopCursor.Tests.Tools
{
    public static partial class Native
    {
        private const string User32 = "user32.dll";

        [DllImport(User32, EntryPoint = "GetCursorInfo")]
        internal static extern bool GetCursorInfo(out Native.CursorInfo pci);


        [DllImport(User32, EntryPoint = "CopyIcon")]
        internal static extern IntPtr CopyIcon(IntPtr hIcon);

        [DllImport(User32, SetLastError = true)]
        internal static extern bool DestroyIcon(IntPtr hIcon);

        [DllImport(User32, EntryPoint = "GetIconInfo")]
        internal static extern bool GetIconInfo(IntPtr hIcon, out Native.IconInfo piconinfo);
        
        [DllImport(User32, SetLastError = false)]
        internal static extern IntPtr GetDesktopWindow();

        [DllImport(User32)]
        internal static extern IntPtr GetWindowDC(IntPtr ptr);

        [DllImport(User32, SetLastError = true)]
        internal static extern bool DrawIconEx(IntPtr hdc, int xLeft, int yTop, IntPtr hIcon, int cxWidth, int cyHeight, int istepIfAniCur, IntPtr hbrFlickerFreeDraw, int diFlags);

        /// <summary>
        /// Releases the device context from the given window handle.
        /// </summary>
        /// <param name="hWnd">The window handle</param>
        /// <param name="hDc">The device context handle.</param>
        /// <returns>True if successful</returns>
        [DllImport(User32)]
        internal static extern bool ReleaseDC(IntPtr hWnd, IntPtr hDc);

    }
}
