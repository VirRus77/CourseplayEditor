using CourseEditor.Drawing.Controllers.Implementation;
using SkiaSharp;

namespace CourseEditor.Drawing.Contract
{
    /// <summary>
    /// Контроллер для управления настройками карты
    /// </summary>
    public interface IMapSettingsController : IValueController<MapSettings>
    {
        void OffsetByMapPoint(in SKPoint mapPoint);

        void OffsetByControlPoint(in SKPoint controlPoint);

        void ZoomByControlPoint(in int zoomDelta, SKPoint position);
    }
}
