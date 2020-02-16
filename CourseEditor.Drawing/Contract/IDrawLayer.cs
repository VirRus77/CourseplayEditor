using System;
using SkiaSharp;

namespace CourseEditor.Drawing.Contract
{
    /// <summary xml:lang="ru-Ru">
    /// Слой для отрисовки.
    /// </summary>
    /// <summary xml:lang="en-US">
    /// Drawing layer.
    /// </summary>
    public interface IDrawLayer
    {
        /// <summary xml:lang="ru-Ru">
        /// <inheritdoc cref="IDrawLayer"/> изменился.
        /// </summary>
        event EventHandler<EventArgs> Changed;

        /// <summary>
        /// <para xml:lang="ru-Ru">Отрисовать.</para>
        /// </summary>
        /// <param name="canvas">
        /// <para xml:lang="ru-Ru">Холст отрисовки</para>
        /// </param>
        /// <param name="drawRect">
        /// <para xml:lang="ru-Ru">Область отрисовки</para>
        /// </param>
        void Draw(SKCanvas canvas, SKRect drawRect);

        /// <summary>
        /// <para xml:lang="ru-Ru">Перерисоваться.</para>
        /// </summary>
        void Invalidate();

        /// <summary>
        /// <para xml:lang="ru-Ru">Видимость слоя</para>
        /// </summary>
        bool IsVisible { get; set; }
    }
}
