using System;
using System.Collections.Generic;
using System.Linq;
using CourseEditor.Drawing.Implementation;
using CourseplayEditor.Contracts;
using CourseplayEditor.Model;
using CourseplayEditor.Tools.Extensions;
using SkiaSharp;

namespace CourseplayEditor.Implementation.Layers
{
    public class CourseDrawLayer : BaseDrawLayer, IDisposable
    {
        public const string DrawCourseLayerKey = "DrawCourseLayer";

        private readonly IManagedDrawSelectableObject _drawSelectableObject;

        public CourseDrawLayer(IManagedDrawSelectableObject drawSelectableObject)
        {
            _drawSelectableObject = drawSelectableObject;
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

            if (Course.Waypoints.Length == 1)
            {
                _drawSelectableObject.DrawCircle(DrawCourseLayerKey, canvas, drawRect, Course.Waypoints.Single().ToSkPoint(), 2f);
                return;
            }

            var points = GeneratePoints(Course);
            _drawSelectableObject.DrawGradientLines(DrawCourseLayerKey, canvas, drawRect, points);
        }

        private void CourseOnChanged(object? sender, EventArgs e)
        {
            RaiseChanged();
        }

        private ICollection<SKPoint> GeneratePoints(Course course)
        {
            return course.Waypoints.Select(v => v.ToSkPoint()).ToArray();
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
