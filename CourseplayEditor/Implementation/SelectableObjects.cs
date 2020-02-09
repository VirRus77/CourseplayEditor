using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Core.Tools.Extensions;
using CourseEditor.Drawing.Contract;
using CourseplayEditor.Contracts;
using CourseplayEditor.Model;
using CourseplayEditor.Tools.Extensions;
using Microsoft.Extensions.Logging;
using SkiaSharp;

namespace CourseplayEditor.Implementation
{
    /// <inheritdoc />
    public class SelectableObjects : SelectableObjects.IModelSelectableObjects
    {
        public interface IModelSelectableObjects : IAddedSelectableObjects<ICollection<Course>>,
                                                   IAddedSelectableObjects<ICollection<SplineMap>>
        {
        }

        private class HashedCourseObject : HashedSelectableObject<Course>
        {
            private ICollection<SKLine> _lines;
            public HashedCourseObject(Course value)
                : base(value)
            {
                _lines = GenerateLines(value);
            }

            private ICollection<SKLine> GenerateLines(Course value)
            {
                if (value.Waypoints.Length < 2)
                {
                    return Array.Empty<SKLine>();
                }

                var firstPoint = value.Waypoints.First().ToSkPoint();
                return value.Waypoints
                     .Skip(1)
                     .Select(
                         v =>
                         {
                             var skPoint = v.ToSkPoint();
                             var skLine = new SKLine(firstPoint, skPoint);
                             firstPoint = skPoint;
                             return skLine;
                         }
                     )
                     .ToArray();
            }

            public override ICollection<ISelectable> Intersect(SKPoint point, float radius)
            {
                if (!_lines.Any(v => v.MinimalDistance(point) <= radius))
                {
                    return Array.Empty<ISelectable>();
                }

                return new ISelectable[]
                {
                    Value
                };
            }
        }

        private class HashedWaypointObject : HashedSelectableObject<Waypoint>
        {
            private SKPoint _point;

            public HashedWaypointObject(Waypoint value)
                : base(value)
            {
                _point = value.ToSkPoint();
            }

            public override ICollection<ISelectable> Intersect(SKPoint point, float radius)
            {
                var intersect = SKPoint.Distance(point, _point) <= radius;
                if (!intersect)
                {
                    return Array.Empty<ISelectable>();
                }

                return new ISelectable[]
                {
                    Value,
                    Value.Course
                };
            }
        }

        private readonly ILogger<SelectableObjects> _logger;
        private readonly ICollection<IHashedSelectableObject> _selectableObjects;

        public SelectableObjects(ILogger<SelectableObjects> logger)
        {
            _logger = logger;
            _selectableObjects = new List<IHashedSelectableObject>();
        }

        public void AddSelectableObjects(ICollection<Course> selectableObjects)
        {
            selectableObjects.ForEach(v => AddSelectableObjects(v));
        }

        private void AddSelectableObjects([NotNull] Course course)
        {
            _selectableObjects.Add(new HashedCourseObject(course));
            _selectableObjects.AddRange(
                course.Waypoints
                      .Select(v => new HashedWaypointObject(v))
            );
        }

        public void AddSelectableObjects(ICollection<SplineMap> selectableObjects)
        {
            //_selectableObjects.AddRange(selectableObjects);
        }

        /// <inheritdoc />
        public ICollection<ISelectable> GetElements(SKPoint point, float radius)
        {
            return _selectableObjects
                   .SelectMany(v => v.Intersect(point, radius))
                   .Distinct()
                   .ToArray();
        }
    }
}
