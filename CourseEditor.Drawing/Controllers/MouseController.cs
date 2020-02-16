using System;
using System.Diagnostics.CodeAnalysis;
using System.Windows;
using System.Windows.Input;
using CourseEditor.Drawing.Contract;
using CourseEditor.Drawing.Controllers.Mouse;
using CourseEditor.Drawing.Tools;
using CourseEditor.Drawing.Tools.Extensions;
using Microsoft.Extensions.Logging;
using CursorType = CourseEditor.Drawing.Contract.CursorType;

namespace CourseEditor.Drawing.Controllers
{
    /// <summary>
    /// Контроллер контролирующий мышку
    /// </summary>
    public class MouseController : IDisposable
    {
        private readonly IMouseOperationManager _mouseOperationManager;
        private readonly IManagerCursor _managerCursor;
        private readonly ILogger<MouseController> _logger;
        private FrameworkElement _frameworkElement;

        public MouseController(
            [NotNull] IMouseOperationManager mouseOperationManager,
            IManagerCursor managerCursor,
            ILogger<MouseController> logger
        )
        {
            _mouseOperationManager = mouseOperationManager;
            _managerCursor = managerCursor;
            _logger = logger;
        }

        public void Init([NotNull] FrameworkElement frameworkElement)
        {
            _frameworkElement = frameworkElement;
            _frameworkElement.MouseMove += ControlOnMouseMove;
            _frameworkElement.MouseUp += ControlOnMouseUp;
            _frameworkElement.MouseDown += ControlOnMouseDown;
            _frameworkElement.MouseWheel += ControlOnMouseWheel;

            _managerCursor.Init(cursor => _frameworkElement.Cursor = cursor);
        }

        private void FrameworkElementOnKeyChanged(object sender, KeyEventArgs e)
        {
            _mouseOperationManager.KeyChanged(e);
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
            if (isCapture && _mouseOperationManager.CurrentOperation != null)
            {
                _frameworkElement.CaptureMouse();
            }
            else
            {
                _frameworkElement.ReleaseMouseCapture();
            }
        }

        public void InitKeyboardHook(FrameworkElement mainWindows)
        {
            Keyboard.AddKeyDownHandler(mainWindows, FrameworkElementOnKeyChanged);
            Keyboard.AddKeyUpHandler(mainWindows, FrameworkElementOnKeyChanged);
        }
    }
}
