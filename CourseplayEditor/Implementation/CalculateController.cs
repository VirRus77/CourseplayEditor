using CourseEditor.Drawing.Controllers.Contract;
using CourseEditor.Drawing.Controllers.Implementation;
using SkiaSharp;

namespace CourseplayEditor.Implementation
{
    /// <inheritdoc />
    public class CalculateController : ICalculateController
    {
        /// <inheritdoc />
        public SKPoint ToMapPoint(MapSettings mapSettings, SKPoint controlPoint)
        {
            return ToMapPoint(mapSettings.PointLeftTop, mapSettings.Scale, controlPoint);
        }

        /// <inheritdoc />
        public SKPoint ToControlPoint(MapSettings mapSettings, SKPoint mapPoint)
        {
            return ToControlPoint(mapSettings.PointLeftTop, mapSettings.Scale, mapPoint);
        }

        /// <inheritdoc />
        public SKPoint ToMapPoint(SKPoint mapPointLeftTop, float scale, SKPoint controlPoint)
        {
            return Mult(controlPoint, scale) + mapPointLeftTop;
        }

        /// <inheritdoc />
        public SKPoint ToControlPoint(SKPoint mapPointLeftTop, float scale, SKPoint mapPoint)
        {
            return Mult(mapPoint - mapPointLeftTop, 1f / scale);
        }

        private static SKPoint Mult(SKPoint point, float mult)
        {
            return new SKPoint(point.X * mult, point.Y * mult);
        }
    }
}
