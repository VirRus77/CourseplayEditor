using System.Windows.Input;
using SkiaSharp;

namespace CourseEditor.Drawing.Controllers.Mouse
{
    public interface IMouseEvents
    {
        bool OnMouseMove(MouseEventArgs mouseEventArgs, SKPoint controlPosition);
        bool OnMouseUp(MouseButtonEventArgs mouseButtonEventArgs, SKPoint controlPosition);
        bool OnMouseDown(MouseButtonEventArgs mouseButtonEventArgs, SKPoint controlPosition);
        bool OnMouseWheel(MouseWheelEventArgs mouseWheelEventArgs, SKPoint controlPosition);
    }
}
