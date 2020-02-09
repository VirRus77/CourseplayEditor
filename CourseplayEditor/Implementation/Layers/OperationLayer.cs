using System.Collections.Generic;
using System.Linq;
using Core.Tools.Extensions;
using CourseEditor.Drawing.Contract;
using CourseEditor.Drawing.Implementation;
using CourseEditor.Drawing.Tools;
using CourseplayEditor.Model;
using SkiaSharp;

namespace CourseplayEditor.Implementation.Layers
{
    public class OperationLayer : BaseDrawLayer
    {
        private readonly ISelectableController _selectableController;
        private readonly IMapSettingsController _mapSettingsController;

        public OperationLayer(ISelectableController selectableController, IMapSettingsController mapSettingsController)
        {
            IsVisible = true;
            _selectableController = selectableController;
            _mapSettingsController = mapSettingsController;
            _selectableController.Changed += SelectableControllerOnChanged;
        }

        public override void Draw(SKCanvas canvas, SKRect drawRect)
        {
            if (!_selectableController.Value.Any())
            {
                return;
            }

            using (var paint = new SKPaint
            {
                Color = new SKColor(255, 0, 220)
            })
            {
                DrawSelectedItems(canvas, drawRect, paint, _selectableController.Value);
            }
        }

        private void DrawSelectedItems(
            SKCanvas canvas,
            SKRect drawRect,
            SKPaint paint,
            ICollection<ISelectable> values
        )
        {
            values
                .OfType<SplineMap>()
                .Where(v => v.Visible)
                .ForEach(v => Draw(canvas, drawRect, paint, v));
            values
                .OfType<Course>()
                .Where(v => v.Visible)
                .ForEach(v => Draw(canvas, drawRect, paint, v));
            values
                .OfType<Waypoint>()
                .Where(v => v.Course.Visible)
                .ForEach(v => Draw(canvas, drawRect, paint, v));
        }

        private void Draw(SKCanvas canvas, SKRect drawRect, SKPaint paint, SplineMap splineMap)
        {
            DrawLines(canvas, drawRect, paint, splineMap.Points.Select(v => ToPoint(v)).ToArray());
            splineMap.Points
                     .Select(v => ToPoint(v))
                     .ForEach(point => DrawPoint(canvas, drawRect, paint, point));
        }

        private void Draw(SKCanvas canvas, SKRect drawRect, SKPaint paint, Course course)
        {
            DrawLines(canvas, drawRect, paint, course.Waypoints.Select(v => ToPoint(v)).ToArray());
            course.Waypoints
                  .Select(v => ToPoint(v))
                  .ForEach(point => DrawPoint(canvas, drawRect, paint, point));
        }

        private void Draw(SKCanvas canvas, SKRect drawRect, SKPaint paint, Waypoint waypoint)
        {
            var point = ToPoint(waypoint);
            DrawPoint(canvas, drawRect, paint, point);
        }

        private void DrawPoint(SKCanvas canvas, SKRect drawRect, SKPaint paint, SKPoint point)
        {
            var rectSize = 5f / _mapSettingsController.Value.Scale;
            canvas.DrawRect(point.X - rectSize / 2f, point.Y - rectSize / 2f, rectSize, rectSize, paint);
        }

        private SKPoint ToPoint(Waypoint point)
        {
            return ToPoint(point.Point);
        }

        private SKPoint ToPoint(SKPoint3 point)
        {
            return new SKPoint(point.X, point.Y);
        }

        private void SelectableControllerOnChanged(object? sender, ValueEventArgs<ICollection<ISelectable>> e)
        {
            RaiseChanged();
        }
    }
}
