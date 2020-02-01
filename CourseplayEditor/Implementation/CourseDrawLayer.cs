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
        private readonly ICollection<Course> _courses;
        private ICollection<SKPath> _path;

        public CourseDrawLayer(ICollection<Course> courses)
        {
            _courses = courses;
        }

        public event EventHandler<EventArgs> Changed;

        public void Draw(SKCanvas canvas, SKRect drawRect)
        {
            _path = _path ?? GeneratePath(_courses);
            using (SKPaint paint = new SKPaint
            {
                Color = new SKColor(255, 0, 0)
            })
            {
                foreach (var course in _courses)
                {
                    if (course.Waypoints.Length < 2)
                    {
                        break;
                    }

                    var points = course.Waypoints.Select(v => ToSkPoint(v));
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
                //_path
                //    .ToList()
                //    .ForEach(v => canvas.DrawPath(v, paint));
            }
        }

        private ICollection<SKPath> GeneratePath(ICollection<Course> courses)
        {
            return courses.ToList()
                          .Select(v => GeneratePath(v))
                          .Where(v => v != null)
                          .ToArray();
        }

        private SKPath GeneratePath(Course courses)
        {
            if (courses.Waypoints.Length == 0)
            {
                return null;
            }

            var path = new SKPath();
            var firstPoint = courses.Waypoints.First();
            var skPoint = ToSkPoint(firstPoint);
            path.MoveTo(skPoint);
            if (courses.Waypoints.Length == 1)
            {
                path.AddCircle(skPoint.X, skPoint.Y, 2);
            }
            else
            {
                courses.Waypoints
                       .Skip(1)
                       .ToList()
                       .ForEach(v => path.LineTo(ToSkPoint(v)));
            }

            return path;
        }

        private static SKPoint ToSkPoint(Waypoint firstPoint)
        {
            return new SKPoint(firstPoint.PointX, firstPoint.PointZ);
        }
    }
}
