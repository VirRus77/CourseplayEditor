using System;
using System.Collections.Generic;
using System.Linq;
using CourseEditor.Drawing.Contract;
using CourseplayEditor.Tools.Courseplay.v2019;
using SkiaSharp;

namespace CourseplayEditor.Implementation
{
    public class CourseDrawLayer : IDrawLayer
    {
        public CourseDrawLayer()
        {
        }

        public void Load(Course course)
        {
            Course = course;
            RaiseChanged();
        }

        public Course Course { get; set; }

        public event EventHandler<EventArgs> Changed;

        void IDrawLayer.Draw(SKCanvas canvas, SKRect drawRect)
        {
            if (Course == null || !Course.Waypoints.Any())
            {
                return;
            }

            using (var paint = new SKPaint
            {
                Color = new SKColor(255, 0, 0)
            })
            {

                if (Course.Waypoints.Length == 1)
                {
                    canvas.DrawCircle(ToSkPoint(Course.Waypoints.Single()), 2f, paint);
                    return;
                }

                var points = GeneratePoints(Course);
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

        private ICollection<SKPoint> GeneratePoints(Course course)
        {
            return course.Waypoints.Select(v => ToSkPoint(v)).ToArray();
        }

        private static SKPoint ToSkPoint(Waypoint firstPoint)
        {
            return new SKPoint(firstPoint.PointX, firstPoint.PointY);
        }

        private void RaiseChanged()
        {
            Changed?.Invoke(this, EventArgs.Empty);
        }
    }
}
