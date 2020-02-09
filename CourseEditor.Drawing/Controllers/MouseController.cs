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
    public class MouseController
    {
        private readonly IMouseOperationManager _mouseOperationManager;
        private readonly ILogger<MouseController> _logger;
        private FrameworkElement _frameworkElement;

        public MouseController(
            IMouseOperationManager mouseOperationManager,
            ILogger<MouseController> logger
        )
        {
            _mouseOperationManager = mouseOperationManager;
            _logger = logger;
        }

        public void Init(FrameworkElement frameworkElement)
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
            _mouseOperationManager.OnMouseMove(e, controlPosition);
        }

        private void ControlOnMouseUp(object sender, MouseButtonEventArgs e)
        {
            var controlPosition = e.GetPosition(_frameworkElement).ToSKPoint();
            _mouseOperationManager.OnMouseUp(e, controlPosition);
        }

        private void ControlOnMouseDown(object sender, MouseButtonEventArgs e)
        {
            var controlPosition = e.GetPosition(_frameworkElement).ToSKPoint();
            _mouseOperationManager.OnMouseDown(e, controlPosition);
        }

        private void ControlOnMouseWheel(object sender, MouseWheelEventArgs e)
        {
            var controlPosition = e.GetPosition(_frameworkElement).ToSKPoint();
            _mouseOperationManager.OnMouseWheel(e, controlPosition);
        }
    }
}
