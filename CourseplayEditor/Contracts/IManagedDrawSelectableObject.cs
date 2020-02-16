using CourseEditor.Drawing.Contract;
using CourseplayEditor.Model;
using SkiaSharp;

namespace CourseplayEditor.Contracts
{
    public interface IManagedDrawSelectableObject : IDrawSelectableObject
    {
        void Draw(string selectableItemsKey, SKCanvas canvas, SKRect drawRect, SplineMap splineMap);
        void Draw(string selectableItemsKey, SKCanvas canvas, SKRect drawRect, Course course);
        void Draw(string selectableItemsKey, SKCanvas canvas, SKRect drawRect, Waypoint waypoint);
    }
}
