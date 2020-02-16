using System.Collections.Generic;
using SkiaSharp;

namespace CourseEditor.Drawing.Contract
{
    /// <summary>
    /// <para lang="ru">Менеджер всех объектов которые могут быть выбранны.</para>
    /// </summary>
    public interface ISelectableObjects
    {
        /// <summary>
        /// <para lang="ru">Получить элементы которые можно выбрать по точке на карте и радиусу.</para>
        /// </summary>
        /// <param name="point">
        /// <para lang="ru">Точка на карте.</para>
        /// </param>
        /// <param name="radius">
        /// <para lang="ru">Радиус от точки в котором искать.</para>
        /// </param>
        /// <returns></returns>
        ICollection<ISelectable> GetElements(SKPoint point, float radius);

        /// <summary xml:lang="ru">
        /// Получить элементы которые можно выбрать по точке на карте и радиусу.
        /// </summary>
        /// <param name="elements" xml:lang="ru">Элементы из которых выбрать.</param>
        /// <param name="point" xml:lang="ru">Точка на карте.</param>
        /// <param name="radius" xml:lang="ru">Радиус от точки в котором искать.</param>
        /// <returns></returns>
        ICollection<ISelectable> GetElements(ICollection<ISelectable> elements, SKPoint point, float radius);

        /// <summary>
        /// <para lang="ru">Получить элементы по области на карте.</para>
        /// </summary>
        /// <param name="rect">
        /// <para lang="ru">Область на карте.</para>
        /// </param>
        /// <returns></returns>
        ICollection<ISelectable> GetElements(SKRect rect);

        /// <summary>
        /// <para lang="ru">Получить элементы которые выделены по точке на карте и радиусу.</para>
        /// </summary>
        /// <param name="point">
        /// <para lang="ru">Точка на карте.</para>
        /// </param>
        /// <param name="radius">
        /// <para lang="ru">Радиус от точки в котором искать.</para>
        /// </param>
        /// <returns></returns>
        ICollection<ISelectable> GetSelectedElements(SKPoint point, float radius);
    }
}
