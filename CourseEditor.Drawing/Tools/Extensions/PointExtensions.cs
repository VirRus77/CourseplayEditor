using System.Windows;
using SkiaSharp;

namespace CourseEditor.Drawing.Tools
{
    public static class PointExtensions
    {
        public static SKPoint ToSKPoint(this Point point)
        {
            return new SKPoint((float) point.X, (float) point.Y);
        }
    }
}
