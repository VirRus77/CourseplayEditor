using CourseplayEditor.Model;
using SkiaSharp;

namespace CourseplayEditor.Tools.Extensions
{
    public static class WaypointExtensions
    {
        public static SKPoint ToSkPoint(this Waypoint firstPoint)
        {
            return new SKPoint(firstPoint.Point.X, firstPoint.Point.Y);
        }
    }
}
