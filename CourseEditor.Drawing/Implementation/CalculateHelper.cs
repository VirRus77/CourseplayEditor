using CourseEditor.Drawing.Contract;
using CourseEditor.Drawing.Controllers.Implementation;
using CourseEditor.Drawing.Tools;
using SkiaSharp;

namespace CourseEditor.Drawing.Implementation
{
    public class CalculateHelper : ICalculateHelper
    {
        private readonly IMapSettingsController _mapSettingsController;
        private MapSettings MapSettings => _mapSettingsController.Value;

        public CalculateHelper(IMapSettingsController mapSettingsController)
        {
            _mapSettingsController = mapSettingsController;
        }

        public SKPoint ToMapPoint(SKPoint controlPoint)
        {
            return CalculatePointHelper.ToMapPoint(MapSettings, controlPoint);
        }

        public float ToMapDistance(float distance)
        {
            return CalculatePointHelper.ToMapDistance(MapSettings, distance);
        }
    }
}
