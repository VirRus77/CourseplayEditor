using System;
using SkiaSharp;

namespace CourseEditor.Drawing.Contract
{
    /// <summary>
    /// Слой для отрисовки
    /// </summary>
    public interface IDrawLayer
    {
        event EventHandler<EventArgs> Changed;

        /// <summary>
        /// Отрисовать
        /// </summary>
        /// <param name="canvas">Холст отрисовки</param>
        /// <param name="drawRect">Область отрисовки</param>
        public void Draw(SKCanvas canvas, SKRect drawRect);
    }
}
