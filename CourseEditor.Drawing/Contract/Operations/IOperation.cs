using SkiaSharp;

namespace CourseEditor.Drawing.Contract.Operations
{
    public interface IOperation
    {
        /// <summary xml:lang="ru">
        /// Длительная операция
        /// </summary>
        bool IsDelayOperation { get; }

        /// <summary xml:lang="ru">
        /// Операция запущена
        /// </summary>
        bool IsRun { get; }

        bool Start(SKPoint controlPoint, SKPoint mapPoint, params object[] args);

        bool Change(SKPoint controlPoint, SKPoint mapPoint, params object[] args);

        bool End(SKPoint controlPoint, SKPoint mapPoint, params object[] args);

        bool Stop(SKPoint controlPoint, SKPoint mapPoint, params object[] args);
    }
}
