using CourseEditor.Drawing.Implementation;
using CourseEditor.Drawing.Tools;
using SkiaSharp;

namespace CourseplayEditor.Implementation.Layers
{
    /// <summary>
    /// Слой для отображения картинки карты
    /// </summary>
    public class BackgroundMapDrawLayer : BaseDrawLayer
    {
        private SKImage _skImage;

        /// <inheritdoc />
        public BackgroundMapDrawLayer()
        {
            IsVisible = true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filePath"></param>
        public void OpenImage(string filePath)
        {
            _skImage = DdsHelper.Load(filePath);
            RaiseChanged();
        }

        /// <inheritdoc />
        public override void Draw(SKCanvas canvas, SKRect drawRect)
        {
            if (!IsVisible || _skImage == null)
            {
                return;
            }
            canvas.DrawImage(_skImage, _skImage.Width / 2f * -1, _skImage.Width / 2f * -1);
        }
    }
}
