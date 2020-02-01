using System.Windows.Controls;
using CourseEditor.Drawing.Contract;
using CourseEditor.Drawing.Controllers.Implementation;
using CourseEditor.Drawing.Tools;
using CourseplayEditor.Contracts;
using SkiaSharp;

namespace CourseplayEditor.Implementation.Control
{
    public class DrawCurrentPosition : TextBlock, IDrawCurrentPosition
    {
        private readonly ICurrentPositionController _controller;
        private readonly IMapSettingsController _mapSettingsController;

        public DrawCurrentPosition(ICurrentPositionController controller, IMapSettingsController mapSettingsController)
            : base()
        {
            _controller = controller;
            _mapSettingsController = mapSettingsController;

            _controller.Changed += ControllerOnPointChanged;
            _mapSettingsController.Changed += MapSettingsControllerOnChanged;
        }

        private void MapSettingsControllerOnChanged(object? sender, ValueEventArgs<MapSettings> e)
        {
            Draw();
        }

        private void ControllerOnPointChanged(object? sender, ValueEventArgs<SKPoint> valueEventArgs)
        {
            Draw();
        }

        private void Draw()
        {
            var point = _controller.Value;
            var mapSettings = _mapSettingsController.Value;
            var mapPoint = CalculatePointHelper.ToMapPoint(mapSettings, point);

            Text = $"{point.X}, {point.Y} ({mapPoint.X}, {mapPoint.Y})\n[{mapSettings.Scale}] {mapSettings.PointLeftTop.X} {mapSettings.PointLeftTop.Y}";
        }
    }
}
