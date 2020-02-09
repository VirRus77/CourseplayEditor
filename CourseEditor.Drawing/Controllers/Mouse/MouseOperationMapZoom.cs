using System.Windows.Input;
using CourseEditor.Drawing.Contract;
using SkiaSharp;

namespace CourseEditor.Drawing.Controllers.Mouse
{
    public class MouseOperationMapZoom : BaseMouseOperation
    {
        private readonly IMapSettingsController _mapSettingsController;

        public MouseOperationMapZoom(IMapSettingsController mapSettingsController)
            : base(MouseOperationType.Zoom, MouseEventType.Wheel)
        {
            _mapSettingsController = mapSettingsController;
        }

        public override bool OnMouseWheel(MouseWheelEventArgs mouseWheelEventArgs, SKPoint controlPosition)
        {
            var zoomDelta = mouseWheelEventArgs.Delta / 120;
            _mapSettingsController.ZoomByControlPoint(zoomDelta, controlPosition);
            return true;
        }
    }
}
