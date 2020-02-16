using System;
using System.Collections.Generic;
using System.Linq;
using CourseEditor.Drawing.Implementation;
using CourseplayEditor.Contracts;
using CourseplayEditor.Model;
using SkiaSharp;

namespace CourseplayEditor.Implementation.Layers
{
    public class SplineDrawLayer : BaseDrawLayer, IDisposable
    {
        private readonly IManagedDrawSelectableObject _drawSelectableObject;
        public const string SplineDrawLayerKey = "SplineDrawLayer";

        public SplineDrawLayer(IManagedDrawSelectableObject drawSelectableObject)
        {
            _drawSelectableObject = drawSelectableObject;
            IsVisible = true;
        }

        public void Load(SplineMap spline)
        {
            if (Spline != null)
            {
                Spline.Changed -= SplineOnChanged;
            }
            Spline = spline;
            Spline.Changed += SplineOnChanged;
            RaiseChanged();
        }

        public SplineMap Spline { get; private set; }

        public override void Draw(SKCanvas canvas, SKRect drawRect)
        {
            if (Spline == null || !Spline.Points.Any() || !Spline.Visible)
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
                    _drawSelectableObject.DrawCircle(SplineDrawLayerKey, canvas, drawRect, ToSkPoint(Spline.Points.Single()), 2f);
                    //canvas.DrawCircle(ToSkPoint(Spline.Points.Single()), 2f, paint);
                    return;
                }

                _drawSelectableObject.DrawLines(SplineDrawLayerKey, canvas, drawRect, GeneratePoints(Spline.Points));

                //var points = GeneratePoints(Spline.Points);
                //var start = points.First();
                //points.Skip(1)
                //      .ToList()
                //      .ForEach(
                //          v =>
                //          {
                //              canvas.DrawLine(start, v, paint);
                //              start = v;
                //          }
                //      );
            }
        }

        private void SplineOnChanged(object? sender, EventArgs e)
        {
            RaiseChanged();
        }

        private static SKPoint ToSkPoint(SKPoint3 point)
        {
            return new SKPoint(point.X, point.Y);
        }

        //private static SKPoint ToSkPoint(I3DVector vector)
        //{
        //    return new SKPoint(vector.X, vector.Z);
        //}

        private static ICollection<SKPoint> GeneratePoints(IEnumerable<SKPoint3> splinePoints)
        {
            return splinePoints
                   .Select(vector => ToSkPoint(vector))
                   .ToArray();
        }

        public void Dispose()
        {
            if (Spline != null)
            {
                Spline.Changed -= SplineOnChanged;
            }
        }
    }
}
