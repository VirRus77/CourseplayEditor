using System;
using SkiaSharp;

namespace CourseEditor.Drawing.Controllers.Contract
{
    public interface IMousePositionController
    {
        event EventHandler<EventArgs> Updated;

        SKPoint MousePosition { get; }

        void Initialize(IDrawControl control);

        void SetPosition(SKPoint position);
    }
}
