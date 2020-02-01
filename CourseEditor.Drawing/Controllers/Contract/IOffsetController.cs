using System;
using SkiaSharp;

namespace CourseEditor.Drawing.Controllers.Contract
{
    /// <summary>
    /// Управление сдвигом
    /// </summary>
    public interface IOffsetController : IController
    {
        event EventHandler<EventArgs> Updated;

        /// <summary>
        /// Координаты левой верхний точки
        /// </summary>
        SKPoint PointLeftTop { get; }
    }
}
