using System;
using CourseEditor.Drawing.Contract;
using SkiaSharp;

namespace CourseEditor.Drawing.Implementation
{
    public class TestDrawLayer : IDrawLayer
    {
        public event EventHandler<EventArgs> Changed;

        public void Draw(SKCanvas canvas, SKRect drawRect)
        {
            canvas.Clear(new SKColor(130, 130, 130));
            canvas.DrawText("SkiaSharp on Wpf!", 50, 200, new SKPaint() { Color = new SKColor(0, 0, 0), TextSize = 100 });
            canvas.DrawRect(new SKRect(-1,-1, 1, 1), new SKPaint(){Color = new SKColor(255, 0, 0) } );
            canvas.DrawCircle(new SKPoint(100,100), 2, new SKPaint(){Color = new SKColor(255, 0, 0) } );
        }
    }
}
