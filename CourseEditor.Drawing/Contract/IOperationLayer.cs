using SkiaSharp;

namespace CourseEditor.Drawing.Contract
{
    public interface IOperationLayer : IDrawLayer
    {
        public delegate void DrawAction(SKCanvas canvas, SKRect drawRect);

        void AddDraw(DrawAction drawAction);

        void RemoveDraw(DrawAction drawAction);
    }
}
