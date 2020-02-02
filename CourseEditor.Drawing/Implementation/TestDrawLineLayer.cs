using System;
using System.Linq;
using CourseEditor.Drawing.Contract;
using SkiaSharp;

namespace CourseEditor.Drawing.Implementation
{
    public class TestDrawLineLayer : IDrawLayer
    {
        private readonly int _countLine;
        private readonly Random _rnd = new Random();

        public TestDrawLineLayer(in int countLine = 10000)
        {
            _countLine = countLine;
        }

        public event EventHandler<EventArgs> Changed;
        public void Draw(SKCanvas canvas, SKRect drawRect)
        {
            using (var paint = new SKPaint { Color = new SKColor(0, 0, 255) })
            {
                foreach (var index in Enumerable.Range(0, _countLine))
                {
                    canvas.DrawLine(GetPoint(), GetPoint(), paint);
                }
            }
        }

        public bool IsVisible { get; set; }

        private SKPoint GetPoint()
        {
            return new SKPoint((float) _rnd.Next(-1000, 1000), (float) _rnd.Next(-1000, 1000));
        }
    }
}
