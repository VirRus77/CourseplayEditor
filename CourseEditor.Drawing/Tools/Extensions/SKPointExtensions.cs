using SkiaSharp;

namespace CourseEditor.Drawing.Tools.Extensions
{
    public static class SKPointExtensions
    {
        public static SKPoint Mult(this SKPoint point, float mult)
        {
            return new SKPoint(point.X * mult, point.Y * mult);
        }
    }
}
