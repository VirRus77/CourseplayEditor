using System.Collections.Generic;
using System.Linq;
using CourseEditor.Drawing.Implementation;
using CourseplayEditor.Model;
using SkiaSharp;

namespace CourseplayEditor.Implementation.Layers
{
    public class CourseDrawLayer : BaseDrawLayer
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
            Course = course;
            RaiseChanged();
        }

        public override void Draw(SKCanvas canvas, SKRect drawRect)
        {
            if (!IsVisible || Course == null || !Course.Waypoints.Any())
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

        private ICollection<SKPoint> GeneratePoints(Course course)
        {
            return course.Waypoints.Select(v => ToSkPoint(v)).ToArray();
        }

        private static SKPoint ToSkPoint(Waypoint firstPoint)
        {
            return new SKPoint(firstPoint.Point.X, firstPoint.Point.Y);
        }
    }
}
