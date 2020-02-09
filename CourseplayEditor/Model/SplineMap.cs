using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using I3dShapes.Model;
using I3dShapes.Model.Primitive;
using SkiaSharp;

namespace CourseplayEditor.Model
{
    /// <inheritdoc />
    public class SplineMap : BaseSelectableModel
    {
        private List<SKPoint3> _points;
        private bool _visible;

        public SplineMap()
        {
            _visible = true;
            _points = new List<SKPoint3>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="spline"></param>
        public SplineMap([NotNull] in Spline spline)
            : this()
        {
            if (spline == null)
            {
                throw new ArgumentNullException(nameof(spline));
            }

            Load(spline);
        }

        public uint Id { get; private set; }

        public string Name { get; private set; }

        public IReadOnlyCollection<SKPoint3> Points => _points;

        public bool Visible
        {
            get => _visible;
            set => SetValue(ref _visible, value);
        }

        private void Load([NotNull] in Spline spline)
        {
            if (spline == null)
            {
                throw new ArgumentNullException(nameof(spline));
            }

            Id = spline.Id;
            Name = spline.Name;
            _points.AddRange(spline.Points.Select(v => ToSKPoint(v)));
        }

        private SKPoint3 ToSKPoint(PointVector vector)
        {
            return new SKPoint3(vector.X, vector.Z, vector.Y);
        }
    }
}
