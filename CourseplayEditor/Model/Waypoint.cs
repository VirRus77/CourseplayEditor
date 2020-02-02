using System;
using JetBrains.Annotations;
using SkiaSharp;

namespace CourseplayEditor.Model
{
    public class Waypoint : BaseSelectableModel
    {
        public Waypoint()
        {
            Point = new SKPoint3();
        }

        public Waypoint([NotNull] Course course, [NotNull] Tools.Courseplay.v2019.Waypoint waypoint)
        {
            Course = course ?? throw new ArgumentNullException(nameof(course));
            if (waypoint == null)
            {
                throw new ArgumentNullException(nameof(waypoint));
            }

            Load(waypoint);
        }

        public int Speed { get; set; }

        public float Angle { get; set; }

        public SKPoint3 Point { get; set; }

        public bool Reverse { get; set; }

        public bool Crossing { get; set; }

        public bool TurnStart { get; set; }

        public bool TurnEnd { get; set; }

        public bool Wait { get; set; }

        public bool Unload { get; set; }

        public bool Generated { get; set; }

        public int Ridgemarker { get; set; }

        [NotNull]
        public Course Course { get; }

        public void Load([NotNull] Tools.Courseplay.v2019.Waypoint waypoint)
        {
            if (waypoint == null)
            {
                throw new ArgumentNullException(nameof(waypoint));
            }

            Speed = waypoint.Speed;
            Angle = waypoint.Angle;
            Point = new SKPoint3(waypoint.PointX, waypoint.PointY, waypoint.PointZ);
            Reverse = ToBool(waypoint.Reverse);
            Crossing = ToBool(waypoint.Crossing);
            TurnStart = ToBool(waypoint.TurnStart);
            TurnEnd = ToBool(waypoint.TurnEnd);
            Wait = ToBool(waypoint.Wait);
            Unload = ToBool(waypoint.Unload);
            Generated = waypoint.Generated;
            Ridgemarker = waypoint.Ridgemarker;
        }

        private bool ToBool(in int value)
        {
            if (value != 0 && value != 1)
            {
                throw new Exception();
            }

            return value == 1;
        }
    }
}
