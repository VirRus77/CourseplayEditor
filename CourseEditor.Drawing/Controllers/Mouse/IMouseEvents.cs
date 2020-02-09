using System.Windows.Input;
using SkiaSharp;

namespace CourseEditor.Drawing.Controllers.Mouse
{
    public interface IMouseEvents
    {
        /// <summary>
        /// Событие мышки
        /// </summary>
        /// <param name="mouseEventArgs"></param>
        /// <param name="controlPosition"></param>
        /// <returns></returns>
        bool OnMouseMove(MouseEventArgs mouseEventArgs, SKPoint controlPosition);

        /// <summary>
        /// Событие мышки
        /// </summary>
        /// <param name="mouseEventArgs"></param>
        /// <param name="controlPosition"></param>
        /// <returns></returns>
        bool OnMouseUp(MouseButtonEventArgs mouseButtonEventArgs, SKPoint controlPosition);

        /// <summary>
        /// Событие мышки
        /// </summary>
        /// <param name="mouseEventArgs"></param>
        /// <param name="controlPosition"></param>
        /// <returns></returns>
        bool OnMouseDown(MouseButtonEventArgs mouseButtonEventArgs, SKPoint controlPosition);

        /// <summary>
        /// Событие мышки
        /// </summary>
        /// <param name="mouseEventArgs"></param>
        /// <param name="controlPosition"></param>
        /// <returns></returns>
        bool OnMouseWheel(MouseWheelEventArgs mouseWheelEventArgs, SKPoint controlPosition);
    }
}
