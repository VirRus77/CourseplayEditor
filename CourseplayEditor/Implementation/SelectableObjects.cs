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
        private class ComparerSelectable : IComparer<ISelectable>
        {
            public int Compare(ISelectable x, ISelectable y)
            {
                if (x is Waypoint)
                {
                    if (y is Course)
                    {
                        return -1;
                    }
                }
                else if (y is Waypoint)
                {
                    return 1;
                }

                return 0;
            }
        }

        public interface IModelSelectableObjects : IAddedSelectableObjects<ICollection<Course>>,
                                                   IAddedSelectableObjects<ICollection<SplineMap>>
        {
        }

        private class HashedCourseObject : HashedSelectableObject<Course>, IHashedSelectableObject<ISelectable>
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

                return new ISelectable[] { Value };
            }

            public override ICollection<ISelectable> Intersect(SKRect rect)
            {
                if (!_lines.All(v => InRect(v, rect)))
                {
                    return Array.Empty<ISelectable>();
                }

                return new ISelectable[] { Value };
            }

            ISelectable IHashedSelectableObject<ISelectable>.Value => Value;
        }

        private class HashedWaypointObject : HashedSelectableObject<Waypoint>, IHashedSelectableObject<ISelectable>
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

                return new ISelectable[] { Value, Value.Course };
            }

            public override ICollection<ISelectable> Intersect(SKRect rect)
            {
                if (!InRect(_point, rect))
                {
                    return Array.Empty<ISelectable>();
                }

                return new ISelectable[] { Value };
            }

            ISelectable IHashedSelectableObject<ISelectable>.Value => Value;
        }

        private readonly ILogger<SelectableObjects> _logger;
        private readonly ICollection<IHashedSelectableObject<ISelectable>> _selectableObjects;

        public SelectableObjects(ILogger<SelectableObjects> logger)
        {
            _logger = logger;
            _selectableObjects = new List<IHashedSelectableObject<ISelectable>>();
        }

        public void AddSelectableObjects(ICollection<Course> selectableObjects)
        {
            selectableObjects.ForEach(v => AddSelectableObjects(v));
        }

        private void AddSelectableObjects([NotNull] Course course)
        {
            _selectableObjects.Add(new HashedCourseObject(course));
            var selectableObjects = _selectableObjects
                .Select(v => v.Value)
                .ToArray();
            _selectableObjects
                .AddRange(
                    course.Waypoints
                        .Where(v => !selectableObjects.Contains(v))
                        .Select(v => new HashedWaypointObject(v))
                        .Cast<IHashedSelectableObject<ISelectable>>()
                );
        }

        public void AddSelectableObjects(ICollection<SplineMap> selectableObjects)
        {
            //_selectableObjects.AddRange(selectableObjects);
        }

        /// <inheritdoc />
        public ICollection<ISelectable> GetElements(SKPoint point, float radius)
        {
            var comparerSelectable = new ComparerSelectable();
            return _selectableObjects
                .SelectMany(v => v.Intersect(point, radius))
                .Distinct()
                .OrderBy(v => v, comparerSelectable)
                .ToArray();
        }

        public ICollection<ISelectable> GetElements(ICollection<ISelectable> elements, SKPoint point, float radius)
        {
            var comparerSelectable = new ComparerSelectable();
            return _selectableObjects
                .Where(v => elements.Contains(v.Value))
                .SelectMany(v => v.Intersect(point, radius))
                .Distinct()
                .OrderBy(v => v, comparerSelectable)
                .ToArray();
        }

        /// <inheritdoc />
        public ICollection<ISelectable> GetElements(SKRect rect)
        {
            var comparerSelectable = new ComparerSelectable();
            return _selectableObjects
                .SelectMany(v => v.Intersect(rect))
                .Distinct()
                .OrderBy(v => v, comparerSelectable)
                .ToArray();
        }

        public ICollection<ISelectable> GetSelectedElements(SKPoint point, float radius)
        {
            var elements = GetElements(point, radius);
            return _selectableObjects
                .Select(v => v.Value)
                .Where(v => elements.Contains(v))
                .ToArray();
        }
    }
}
