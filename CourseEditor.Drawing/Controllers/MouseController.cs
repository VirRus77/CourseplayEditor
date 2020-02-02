using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Input;
using CourseEditor.Drawing.Contract;
using CourseEditor.Drawing.Tools;
using Microsoft.Extensions.Logging;
using SkiaSharp;

namespace CourseEditor.Drawing.Controllers
{
    /// <summary>
    /// Контроллер контролирующий мышку
    /// </summary>
    public class MouseController
    {
        private readonly IMapSettingsController _mapSettingsController;
        private readonly ICurrentPositionController _currentPositionController;
        private readonly ILogger<MouseController> _logger;
        private FrameworkElement _frameworkElement;
        private MouseOperation _currentOperation;


        public enum MouseOperationType
        {
            Move
        }

        private class MouseOperation
        {
            public MouseOperation(MouseOperationType mouseOperation, SKPoint startPoint)
            {
                OperationType = mouseOperation;
                StartPoint = startPoint;
                LastDeltaPoint = StartPoint;
            }

            public MouseOperationType OperationType { get; }

            public SKPoint StartPoint { get; }
            public SKPoint LastDeltaPoint { get; private set; }

            public SKPoint Delta(SKPoint currentPoint)
            {
                var delta = LastDeltaPoint - currentPoint;
                LastDeltaPoint = currentPoint;
                return delta;
            }

            public void End(SKPoint endPoint)
            {
            }
        }

        public MouseController(
            IMapSettingsController mapSettingsController,
            ICurrentPositionController currentPositionController,
            ISelectableController selectableController,
            ILogger<MouseController> logger
        )
        {
            _mapSettingsController = mapSettingsController;
            _currentPositionController = currentPositionController;
            _logger = logger;
        }

        public void Init(FrameworkElement frameworkElement)
        {
            _frameworkElement = frameworkElement;
            _frameworkElement.MouseMove += ControlOnMouseMove;
            _frameworkElement.MouseDown += ControlOnMouseDown;
            _frameworkElement.MouseUp += ControlOnMouseUp;
            _frameworkElement.MouseWheel += ControlOnMouseWheel;
        }

        private void ControlOnMouseWheel(object sender, MouseWheelEventArgs e)
        {
            var position = _currentPositionController.Value;
            var zoomDelta = e.Delta / 120;
            _mapSettingsController.ZoomByControlPoint(zoomDelta, position);
        }

        private void ControlOnMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Middle && e.ButtonState == MouseButtonState.Pressed)
            {
                _logger.LogInformation($"Start {nameof(MouseOperationType.Move)}");
                _currentOperation = new MouseOperation(MouseOperationType.Move, _currentPositionController.Value);
                return;
            }
        }

        private void ControlOnMouseUp(object sender, MouseButtonEventArgs e)
        {
            if (_currentOperation == null)
            {
                return;
            }

            switch (_currentOperation.OperationType)
            {
                case MouseOperationType.Move:
                    if (e.ChangedButton != MouseButton.Middle || e.ButtonState != MouseButtonState.Released)
                    {
                        return;
                    }

                    _currentOperation = null;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            {
            }
        }

        private void ControlOnMouseMove(object sender, MouseEventArgs e)
        {
            var controlPosition = e.GetPosition(_frameworkElement).ToSKPoint();
            _currentPositionController?.SetValue(controlPosition);

            if (_currentOperation != null)
            {
                var delta = _currentOperation.Delta(controlPosition);
                switch (_currentOperation.OperationType)
                {
                    case MouseOperationType.Move:
                        Debug.WriteLine($"Delta control point {delta}");
                        _mapSettingsController.OffsetByControlPoint(delta);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }
    }
}
