using System.Collections.Generic;
using SkiaSharp;

namespace CourseEditor.Drawing.Contract
{
    public interface IDrawSelectableObject
    {
        void DrawCircle(string drawLayerKey, SKCanvas canvas, SKRect drawRect, SKPoint centerPoint, float radius);

        void DrawLines(string drawLayerKey, SKCanvas canvas, SKRect drawRect, ICollection<SKPoint> points);

        void DrawGradientLines(string drawLayerKey, SKCanvas canvas, SKRect drawRect, ICollection<SKPoint> points);
    }
}
