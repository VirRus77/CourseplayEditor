using System;
using System.Diagnostics.CodeAnalysis;
using System.Windows;
using System.Windows.Input;
using CourseEditor.Drawing.Controllers.Mouse;
using CourseEditor.Drawing.Tools;
using Microsoft.Extensions.Logging;

namespace CourseEditor.Drawing.Controllers
{
    /// <summary>
    /// Контроллер контролирующий мышку
    /// </summary>
    public class MouseController : IDisposable
    {
        private readonly IMouseOperationManager _mouseOperationManager;
        private readonly ILogger<MouseController> _logger;
        private FrameworkElement _frameworkElement;

        public MouseController(
            [NotNull] IMouseOperationManager mouseOperationManager,
            ILogger<MouseController> logger
        )
        {
            _mouseOperationManager = mouseOperationManager;
            _logger = logger;
        }

        public void Init([NotNull] FrameworkElement frameworkElement)
        {
            _frameworkElement = frameworkElement;
            _frameworkElement.MouseMove += ControlOnMouseMove;
            _frameworkElement.MouseUp += ControlOnMouseUp;
            _frameworkElement.MouseDown += ControlOnMouseDown;
            _frameworkElement.MouseWheel += ControlOnMouseWheel;
        }

        private void ControlOnMouseMove(object sender, MouseEventArgs e)
        {
            var controlPosition = e.GetPosition(_frameworkElement).ToSKPoint();
            var isRun = _mouseOperationManager.OnMouseMove(e, controlPosition);

            MouseCapture(isRun);
        }

        private void ControlOnMouseUp(object sender, MouseButtonEventArgs e)
        {
            var controlPosition = e.GetPosition(_frameworkElement).ToSKPoint();
            var isRun = _mouseOperationManager.OnMouseUp(e, controlPosition);

            MouseCapture(isRun);
        }

        private void ControlOnMouseDown(object sender, MouseButtonEventArgs e)
        {
            var controlPosition = e.GetPosition(_frameworkElement).ToSKPoint();
            var isRun = _mouseOperationManager.OnMouseDown(e, controlPosition);

            MouseCapture(isRun);
        }

        private void ControlOnMouseWheel(object sender, MouseWheelEventArgs e)
        {
            var controlPosition = e.GetPosition(_frameworkElement).ToSKPoint();
            var isRun = _mouseOperationManager.OnMouseWheel(e, controlPosition);

            MouseCapture(isRun);
        }
        private void ControlOnLostMouseCapture(object sender, MouseEventArgs e)
        {
            _mouseOperationManager.StopOperation();
            MouseCapture(false);
        }

        public void Dispose()
        {
            _frameworkElement.MouseMove -= ControlOnMouseMove;
            _frameworkElement.MouseUp -= ControlOnMouseUp;
            _frameworkElement.MouseDown -= ControlOnMouseDown;
            _frameworkElement.MouseWheel -= ControlOnMouseWheel;
            _frameworkElement.LostMouseCapture -= ControlOnLostMouseCapture;
        }

        private void MouseCapture(bool isCapture)
        {
            if (isCapture && _mouseOperationManager.CurrentMouseOperation != null)
            {
                _frameworkElement.CaptureMouse();
            }
            else
            {
                _frameworkElement.ReleaseMouseCapture();
            }
        }
    }
}
