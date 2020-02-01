using CourseEditor.Drawing.Controllers.Implementation;
using SkiaSharp;

namespace CourseEditor.Drawing.Controllers.Contract
{
    /// <summary>
    /// Конвертатор точек
    /// </summary>
    public interface ICalculateController
    {
        /// <summary>
        /// Конвертация в точку карты
        /// </summary>
        /// <param name="mapSettings">Настройки карты хранящие Левая верхняя точка карты и Масштаб</param>
        /// <param name="controlPoint">Точка в координатах контрола</param>
        /// <returns></returns>
        SKPoint ToMapPoint(MapSettings mapSettings, SKPoint controlPoint);

        /// <summary>
        /// Конвертация в точку карты
        /// </summary>
        /// <param name="mapSettings">Настройки карты хранящие Левая верхняя точка карты и Масштаб</param>
        /// <param name="controlPoint">Точка в координатах контрола</param>
        /// <returns></returns>
        SKPoint ToControlPoint(MapSettings mapSettings, SKPoint mapPoint);

        /// <summary>
        /// Конвертация в точку карты
        /// </summary>
        /// <param name="mapPointLeftTop">Левая верхняя точка карты</param>
        /// <param name="scale">Масштаб</param>
        /// <param name="controlPoint">Точка в координатах контрола</param>
        /// <returns></returns>
        SKPoint ToMapPoint(SKPoint mapPointLeftTop, float scale, SKPoint controlPoint);

        /// <summary>
        /// Конвертация в точку контрола
        /// </summary>
        /// <param name="mapPointLeftTop">Левая верхняя точка карты</param>
        /// <param name="scale">Масштаб</param>
        /// <param name="controlPoint">Точка в координатах карты</param>
        /// <returns></returns>
        SKPoint ToControlPoint(SKPoint mapPointLeftTop, float scale, SKPoint mapPoint);
    }
}
