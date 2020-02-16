using System.Windows.Input;
using CourseEditor.Drawing.Contract;
using CourseEditor.Drawing.Contract.Operations;
using CourseEditor.Drawing.Tools;
using Microsoft.Extensions.Logging;
using SkiaSharp;

namespace CourseEditor.Drawing.Controllers.Mouse
{
    public class MouseOperationMapMove : BaseMouseOperation
    {
        private readonly IMapSettingsController _mapSettingsController;
        private readonly IMapMoveOperation _mapMoveOperation;
        private readonly ILogger<MouseOperationMapMove> _logger;

        public MouseOperationMapMove(
            IMapSettingsController mapSettingsController,
            IMapMoveOperation mapMoveOperation,
            ILogger<MouseOperationMapMove> logger
        )
            : base(MouseOperationType.Move, MouseEventType.Move | MouseEventType.Up | MouseEventType.Down)
        {
            _mapSettingsController = mapSettingsController;
            _mapMoveOperation = mapMoveOperation;
            _logger = logger;
        }

        protected void Delta(SKPoint currentPoint)
        {
            var delta = LastDeltaPoint - currentPoint;
            LastDeltaPoint = currentPoint;

            _mapSettingsController.OffsetByControlPoint(delta);
        }

        public override bool OnMouseDown(MouseButtonEventArgs mouseButtonEventArgs, SKPoint controlPosition)
        {
            if (mouseButtonEventArgs.ChangedButton != MouseButton.Middle ||
                mouseButtonEventArgs.ButtonState != MouseButtonState.Pressed)
            {
                return false;
            }

            IsRun = _mapMoveOperation.Start(
                controlPosition,
                CalculatePointHelper.ToMapPoint(_mapSettingsController.Value, controlPosition)
            );
            _logger.LogInformation("MouseOperationMapMove OnMouseDown IsRun: {IsRun}", IsRun);
            return IsRun;
        }

        public override bool OnMouseMove(MouseEventArgs mouseEventArgs, SKPoint controlPosition)
        {
            IsRun = IsRun && (mouseEventArgs.MiddleButton == MouseButtonState.Pressed);
            _logger.LogInformation("MouseOperationMapMove OnMouseMove IsRun: {IsRun}", IsRun);
            _logger.LogInformation("_mapMoveOperation IsRun: {IsRun}", _mapMoveOperation.IsRun);
            if (!_mapMoveOperation.IsRun)
            {
                return false;
            }

            if (mouseEventArgs.MiddleButton != MouseButtonState.Pressed || !IsRun)
            {
                return _mapMoveOperation.Stop(
                    controlPosition,
                    CalculatePointHelper.ToMapPoint(_mapSettingsController.Value, controlPosition)
                );
            }

            return _mapMoveOperation.Change(
                controlPosition,
                CalculatePointHelper.ToMapPoint(_mapSettingsController.Value, controlPosition)
            );
        }

        public override bool OnMouseUp(MouseButtonEventArgs mouseButtonEventArgs, SKPoint controlPosition)
        {
            if (!_mapMoveOperation.IsRun ||
                mouseButtonEventArgs.ChangedButton != MouseButton.Middle ||
                mouseButtonEventArgs.ButtonState != MouseButtonState.Released)
            {
                return false;
            }

            End(controlPosition);
            return true;
        }
    }
}
