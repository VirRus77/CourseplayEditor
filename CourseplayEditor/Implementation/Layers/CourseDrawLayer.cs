using System;
using System.Collections.Generic;
using System.Linq;
using CourseEditor.Drawing.Implementation;
using CourseplayEditor.Model;
using SkiaSharp;

namespace CourseplayEditor.Implementation.Layers
{
    public class CourseDrawLayer : BaseDrawLayer, IDisposable
    {
        public CourseDrawLayer()
        {
            IsVisible = true;
        }

        /// <summary>
        /// 
        /// </summary>
        public Course Course { get; private set; }

        public void Load(Course course)
        {
            if (Course != null)
            {
                Course.Changed -= CourseOnChanged;
            }

            Course = course;
            Course.Changed += CourseOnChanged;
            RaiseChanged();
        }

        public override void Draw(SKCanvas canvas, SKRect drawRect)
        {
            if (!IsVisible || Course == null || !Course.Waypoints.Any() || !Course.Visible)
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
                DrawLines(canvas, drawRect, paint, points);
            }
        }

        private void CourseOnChanged(object? sender, EventArgs e)
        {
            RaiseChanged();
        }

        private ICollection<SKPoint> GeneratePoints(Course course)
        {
            return course.Waypoints.Select(v => ToSkPoint(v)).ToArray();
        }

        private static SKPoint ToSkPoint(Waypoint firstPoint)
        {
            return new SKPoint(firstPoint.Point.X, firstPoint.Point.Y);
        }

        public void Dispose()
        {
            if (Course != null)
            {
                Course.Changed -= CourseOnChanged;

            }
        }
    }
}
