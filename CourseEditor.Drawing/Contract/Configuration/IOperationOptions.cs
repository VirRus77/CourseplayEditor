namespace CourseEditor.Drawing.Contract.Configuration
{
    /// <summary xml:lang="ru">
    /// Настройки для операций.
    /// </summary>
    /// <summary xml:lang="en">
    /// Operation settings.
    /// </summary>
    internal interface IOperationOptions
    {
        /// <summary xml:lang="ru">
        /// Фактор масштаба.
        /// </summary>
        /// <summary xml:lang="en">
        /// Zoom scale.
        /// </summary>
        float ScaleFactor { get; }

        /// <summary xml:lang="ru">
        /// Радиус выделения объектов (px).
        /// </summary>
        /// <summary xml:lang="en">
        /// Radius selection of objects (px).
        /// </summary>
        public int SelectRadius { get; }

        /// <summary xml:lang="ru">
        /// Радиус смещения с которого начнётся выделение мышкой (Draw px).
        /// </summary>
        /// <summary xml:lang="en">
        /// The radius of the start of the rectangle select (Draw px).
        /// </summary>
        int RectangleToleranceRadius { get; }

        /// <summary xml:lang="ru">
        /// Радиус с которого начнётся перемещение мышкой (Draw px).
        /// </summary>
        /// <summary xml:lang="en">
        /// The radius of the start of the move select objects (Draw px).
        /// </summary>
        int MoveToleranceRadius { get; }
    }
}
