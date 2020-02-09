using CourseEditor.Drawing.Controllers.Implementation;
using SkiaSharp;

namespace CourseEditor.Drawing.Tools
{
    /// <inheritdoc />
    public static class CalculatePointHelper
    {
        /// <inheritdoc />
        public static SKPoint ToMapPoint(MapSettings mapSettings, SKPoint controlPoint)
        {
            return ToMapPoint(mapSettings.PointLeftTop, mapSettings.Scale, controlPoint);
        }

        public static float ToMapDistance(MapSettings mapSettings, float controlPoint)
        {
            return controlPoint * 1f / mapSettings.Scale;
        }

        /// <inheritdoc />
        public static SKPoint ToDeltaMapPoint(MapSettings mapSettings, SKPoint controlPoint)
        {
            return ToDeltaMapPoint(mapSettings.PointLeftTop, mapSettings.Scale, controlPoint);
        }

        /// <inheritdoc />
        public static SKPoint ToControlPoint(MapSettings mapSettings, SKPoint mapPoint)
        {
            return ToControlPoint(mapSettings.PointLeftTop, mapSettings.Scale, mapPoint);
        }
        /// <inheritdoc />
        public static SKPoint ToDeltaControlPoint(MapSettings mapSettings, SKPoint controlPoint)
        {
            return ToDeltaControlPoint(mapSettings.PointLeftTop, mapSettings.Scale, controlPoint);
        }

        /// <inheritdoc />
        public static SKPoint ToMapPoint(SKPoint mapPointLeftTop, float scale, SKPoint controlPoint)
        {
            var scalePoint = Mult(controlPoint, 1f / scale);
            return mapPointLeftTop + scalePoint;
        }

        /// <inheritdoc />
        public static SKPoint ToDeltaMapPoint(SKPoint mapPointLeftTop, float scale, SKPoint controlPoint)
        {
            return Mult(controlPoint, 1f / scale);
        }

        /// <inheritdoc />
        public static SKPoint ToControlPoint(SKPoint mapPointLeftTop, float scale, SKPoint mapPoint)
        {
            return Mult(mapPoint - mapPointLeftTop, 1f / scale);
        }

        /// <inheritdoc />
        public static SKPoint ToDeltaControlPoint(SKPoint mapPointLeftTop, float scale, SKPoint mapPoint)
        {
            return Mult(mapPoint, 1f / scale);
        }

        private static SKPoint Mult(SKPoint point, float mult)
        {
            return new SKPoint(point.X * mult, point.Y * mult);
        }
    }
}
