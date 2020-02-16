using System;
using System.Runtime.InteropServices;

namespace DevelopCursor.Tests.Tools
{
    public static partial class Native
    {
        [StructLayout(LayoutKind.Sequential)]
        internal struct IconInfo
        {
            public bool fIcon;      // Specifies whether this structure defines an icon or a cursor. A value of TRUE specifies 
            public int xHotspot;    // Specifies the x-coordinate of a cursor's hot spot. If this structure defines an icon, the hot 
            public int yHotspot;    // Specifies the y-coordinate of the cursor's hot spot. If this structure defines an icon, the hot 
            public IntPtr hbmMask;  // (HBITMAP) Specifies the icon bitmask bitmap. If this structure defines a black and white icon, 
            public IntPtr hbmColor; // (HBITMAP) Handle to the icon color bitmap. This member can be optional if this 
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct PointW
        {
            public int X;
            public int Y;
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct CursorInfo
        {
            /// <summary>
            /// Specifies the size, in bytes, of the structure. 
            /// </summary>
            public int cbSize;

            /// <summary>
            /// Specifies the cursor state. This parameter can be one of the following values:
            /// </summary>
            public int flags;

            ///<summary>
            ///Handle to the cursor. 
            ///</summary>
            public IntPtr hCursor;

            /// <summary>
            /// A POINT structure that receives the screen coordinates of the cursor. 
            /// </summary>
            public PointW ptScreenPos;
        }
    }
}
