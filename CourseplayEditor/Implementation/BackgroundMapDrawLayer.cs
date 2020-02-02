using System;
using CourseEditor.Drawing.Contract;
using CourseEditor.Drawing.Tools;
using SkiaSharp;

namespace CourseplayEditor.Implementation
{
    /// <summary>
    /// Слой для отображения картинки карты
    /// </summary>
    public class BackgroundMapDrawLayer : IDrawLayer
    {
        private SKImage _skImage;

        public BackgroundMapDrawLayer()
        {
        }

        public event EventHandler<EventArgs> Changed;

        public void OpenImage(string filePath)
        {
            _skImage = DdsHelper.Load(filePath);
            RaiseChanged();
        }

        public void Draw(SKCanvas canvas, SKRect drawRect)
        {
            if (_skImage == null)
            {
                return;
            }
            canvas.DrawImage(_skImage, _skImage.Width / 2f * -1, _skImage.Width / 2f * -1);
        }

        private void RaiseChanged()
        {
            Changed?.Invoke(this, EventArgs.Empty);
        }
    }
}
