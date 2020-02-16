using SkiaSharp;

namespace CourseEditor.Drawing.Contract
{
    public interface ICalculateHelper
    {
        SKPoint ToMapPoint(SKPoint controlPoint);
        float ToMapDistance(float distance);
    }
}
