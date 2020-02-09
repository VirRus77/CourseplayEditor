using System.Collections.Generic;
using SkiaSharp;

namespace CourseEditor.Drawing.Contract
{
    /// <summary>
    /// Менеджер всех объектов которые могут быть выбранны
    /// </summary>
    public interface ISelectableObjects
    {
        /// <summary>
        /// Получить элементы которые можно выбрать по точке на карте и радиусу.
        /// </summary>
        /// <param name="point">Точка на карте.</param>
        /// <param name="radius">Радиус от точки в котором искать.</param>
        /// <returns></returns>
        ICollection<ISelectable> GetElements(SKPoint point, float radius);
    }
}
