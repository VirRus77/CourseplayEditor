using System;
using System.Collections.Generic;
using System.Linq;
using CourseEditor.Drawing.Contract;
using I3DShapesTool.Lib.Model;
using SkiaSharp;

namespace CourseplayEditor.Implementation
{
    public class SplineDrawLayer : IDrawLayer
    {
        public void Load(Spline spline)
        {
            Spline = spline;
            RaiseChanged();
        }

        public Spline Spline { get; private set; }

        public event EventHandler<EventArgs> Changed;
        public void Draw(SKCanvas canvas, SKRect drawRect)
        {
            if (Spline == null || !Spline.Points.Any())
            {
                return;
            }

            using (var paint = new SKPaint
            {
                Color = new SKColor(0, 255, 0)
            })
            {
                if (Spline.Points.Count == 1)
                {
                    canvas.DrawCircle(ToSkPoint(Spline.Points.Single()), 2f, paint);
                    return;
                }

                var points = GeneratePoints(Spline.Points);
                var start = points.First();
                points.Skip(1)
                      .ToList()
                      .ForEach(
                          v =>
                          {
                              canvas.DrawLine(start, v, paint);
                              start = v;
                          });

            }
        }

        private void RaiseChanged()
        {
            Changed?.Invoke(this, EventArgs.Empty);
        }

        private static SKPoint ToSkPoint(I3DVector vector)
        {
            return new SKPoint(vector.X, vector.Z);
        }

        private static ICollection<SKPoint> GeneratePoints(ICollection<I3DVector> splinePoints)
        {
            return splinePoints.Select(vector => ToSkPoint(vector))
                               .ToArray();
        }
    }
}
