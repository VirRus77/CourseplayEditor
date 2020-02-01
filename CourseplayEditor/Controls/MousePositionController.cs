using System;
using System.Windows;
using CourseEditor.Drawing;
using CourseEditor.Drawing.Controllers.Contract;
using SkiaSharp;

namespace CourseplayEditor.Controls
{
    class MousePositionController : IMousePositionController
    {
        public event EventHandler<EventArgs> Updated;

        public SKPoint MousePosition { get; }

        public void Initialize(IDrawControl control)
        {
            if (!(control is FrameworkElement framework))
            {
                throw new ArgumentException(nameof(control), $"Is not instance {nameof(FrameworkElement)}");
                //return;
            }

            throw new NotImplementedException();
        }

        public void SetPosition(SKPoint position)
        {
            throw new NotImplementedException();
        }
    }
}
