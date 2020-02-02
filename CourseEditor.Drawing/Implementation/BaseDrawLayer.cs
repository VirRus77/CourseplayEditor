using System;
using System.Collections.Generic;
using System.Linq;
using CourseEditor.Drawing.Contract;
using SkiaSharp;

namespace CourseEditor.Drawing.Implementation
{
    /// <inheritdoc />
    public abstract class BaseDrawLayer : IDrawLayer
    {
        private bool _isVisible;

        /// <inheritdoc />
        public event EventHandler<EventArgs> Changed;

        void IDrawLayer.Draw(SKCanvas canvas, SKRect drawRect)
        {
            if (!IsVisible)
            {
                return;
            }

            Draw(canvas, drawRect);
        }

        public abstract void Draw(SKCanvas canvas, SKRect drawRect);

        /// <inheritdoc />
        public bool IsVisible
        {
            get => _isVisible;
            set => SetValue(ref _isVisible, value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="newValue"></param>
        /// <typeparam name="T"></typeparam>
        protected void SetValue<T>(ref T value, in T newValue)
        {
            if (Equals(value, newValue))
            {
                return;
            }

            value = newValue;
            RaiseChanged();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="canvas"></param>
        /// <param name="drawRect"></param>
        /// <param name="paint"></param>
        /// <param name="points"></param>
        protected void DrawLines(SKCanvas canvas, SKRect drawRect, SKPaint paint, ICollection<SKPoint> points)
        {
            var start = points.First();
            points.Skip(1)
                  .ToList()
                  .ForEach(
                      v =>
                      {
                          canvas.DrawLine(start, v, paint);
                          start = v;
                      }
                  );
        }

        /// <summary>
        /// Вызвать <see cref="Changed"/>
        /// </summary>
        protected virtual void RaiseChanged()
        {
            Changed?.Invoke(this, EventArgs.Empty);
        }
    }
}
