using SkiaSharp;

namespace CourseEditor.Drawing.Contract.Operations
{
    public interface IMapZoomOperation : IOperation
    {
        bool Zoom(SKPoint controlPoint, SKPoint mapPoint, int delta);
    }
}
