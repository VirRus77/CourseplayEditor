using System;
using System.Diagnostics;
using CourseEditor.Drawing.Contract;
using CourseEditor.Drawing.Controllers.Implementation;
using CourseEditor.Drawing.Tools;
using SkiaSharp;

namespace CourseEditor.Drawing.Implementation
{
    /// <inheritdoc />
    public class MapSettingsController : ValueController<MapSettings>, IMapSettingsController
    {
        private float ZoomFactor = 1.25f;

        private MapSettings MapSettings => Value;
        private SKPoint PointLeftTop => MapSettings.PointLeftTop;
        private float Scale => MapSettings.Scale;

        public MapSettingsController()
            : base(new MapSettings(new SKPoint(0, 0), 2f))
        {
        }

        public void OffsetByMapPoint(in SKPoint mapPoint)
        {
            SetValue(new MapSettings(mapPoint, Scale));
        }

        public void OffsetByControlPoint(in SKPoint controlPoint)
        {
            var delta = CalculatePointHelper.ToMapPoint(MapSettings, controlPoint);
            OffsetByMapPoint(delta);
        }

        public void ZoomByControlPoint(in int zoomDelta, SKPoint position)
        {
            var newScale = Scale * (float)Math.Pow(ZoomFactor, zoomDelta);
            Debug.WriteLine($"new sacle: {newScale}");

            var zoomMapPosition = CalculatePointHelper.ToMapPoint(MapSettings, position);
            Debug.WriteLine($"zoom map position: {zoomMapPosition}");

            var deltaMapPoint = CalculatePointHelper.ToDeltaMapPoint(SKPoint.Empty, newScale, position);
            Debug.WriteLine($"deltaMapPoint new scale: {deltaMapPoint}");

            var newLeftTopMapPosition = zoomMapPosition - deltaMapPoint;
            Debug.WriteLine($"newLeftTopMapPosition : {newLeftTopMapPosition}");
            SetValue(new MapSettings(newLeftTopMapPosition, newScale));
        }
    }
}
