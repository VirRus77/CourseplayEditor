using SkiaSharp;

namespace CourseEditor.Drawing.Controllers.Implementation
{
    /// <summary>
    /// Настройки карты хранящие <inheritdoc cref="PointLeftTop"/> и <inheritdoc cref="Scale"/>
    /// </summary>
    public class MapSettings
    {
        /// <summary>
        /// Конструктор <inheritdoc cref="MapSettings"/>
        /// </summary>
        /// <param name="point"><inheritdoc cref="PointLeftTop"/></param>
        /// <param name="scale"><inheritdoc cref="Scale"/></param>
        public MapSettings(SKPoint point = new SKPoint(), float scale = 1f)
        {
            PointLeftTop = point;
            Scale = scale;
        }

        /// <summary>
        /// Левая верхняя точка в координатах карты
        /// </summary>
        public SKPoint PointLeftTop { get; }

        /// <summary>
        /// Масштаб карты
        /// </summary>
        public float Scale { get; }
    }
}
