using System;
using SkiaSharp;

namespace CourseEditor.Drawing.Contract
{
    /// <summary>
    /// Слой для отрисовки
    /// </summary>
    public interface IDrawLayer
    {
        /// <summary>
        /// <inheritdoc cref="IDrawLayer"/> изменился
        /// </summary>
        event EventHandler<EventArgs> Changed;

        /// <summary>
        /// Отрисовать
        /// </summary>
        /// <param name="canvas">Холст отрисовки</param>
        /// <param name="drawRect">Область отрисовки</param>
        void Draw(SKCanvas canvas, SKRect drawRect);

        /// <summary>
        /// Видимость слоя
        /// </summary>
        bool IsVisible { get; set; }
    }
}
