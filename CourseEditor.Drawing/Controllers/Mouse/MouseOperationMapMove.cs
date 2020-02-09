using System.Windows.Input;
using CourseEditor.Drawing.Contract;
using SkiaSharp;

namespace CourseEditor.Drawing.Controllers.Mouse
{
    public class MouseOperationMapMove : BaseMouseOperation
    {
        private readonly IMapSettingsController _mapSettingsController;

        public MouseOperationMapMove(IMapSettingsController mapSettingsController)
            : base(MouseOperationType.Move, MouseEventType.Move | MouseEventType.Up | MouseEventType.Down)
        {
            _mapSettingsController = mapSettingsController;
        }

        protected void Delta(SKPoint currentPoint)
        {
            var delta = LastDeltaPoint - currentPoint;
            LastDeltaPoint = currentPoint;

            _mapSettingsController.OffsetByControlPoint(delta);
        }

        public override bool OnMouseDown(MouseButtonEventArgs mouseButtonEventArgs, SKPoint controlPosition)
        {
            if (!mouseButtonEventArgs.ChangedButton.HasFlag(MouseButton.Middle) ||
                mouseButtonEventArgs.ButtonState != MouseButtonState.Pressed)
            {
                return false;
            }

            Start(controlPosition); 
            return true;
        }

        public override bool OnMouseMove(MouseEventArgs mouseEventArgs, SKPoint controlPosition)
        {
            if (!IsRun)
            {
                return false;
            }

            Delta(controlPosition);
            return true;
        }

        public override bool OnMouseUp(MouseButtonEventArgs mouseButtonEventArgs, SKPoint controlPosition)
        {
            if (!IsRun || !mouseButtonEventArgs.ChangedButton.HasFlag(MouseButton.Middle) ||
                mouseButtonEventArgs.ButtonState != MouseButtonState.Released)
            {
                return false;
            }

            End(controlPosition);
            return true;
        }
    }
}
