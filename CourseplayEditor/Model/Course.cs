using System;
using System.Linq;
using JetBrains.Annotations;

namespace CourseplayEditor.Model
{
    public class Course : BaseSelectableModel
    {
        private string _name;
        private float _workWidth;
        private int _numHeadlandLanes;
        private bool _headlandDirectionCw;
        private bool _visible;

        public Course()
        {
            _visible = true;
        }

        public Course(in Tools.Courseplay.v2019.Course course)
            : this()
        {
            Load(course);
        }

        public string Name
        {
            get => _name;
            set => SetValue(ref _name, value);
        }

        public float WorkWidth
        {
            get => _workWidth;
            set => SetValue(ref _workWidth, value);
        }

        public int NumHeadlandLanes
        {
            get => _numHeadlandLanes;
            set => SetValue(ref _numHeadlandLanes, value);
        }

        public bool HeadlandDirectionCW
        {
            get => _headlandDirectionCw;
            set => SetValue(ref _headlandDirectionCw, value);
        }

        public Waypoint[] Waypoints { get; set; } = new Waypoint[0];

        public bool Visible
        {
            get => _visible;
            set => SetValue(ref _visible, value);
        }

        public void Load([NotNull] Tools.Courseplay.v2019.Course course)
        {
            if (course == null)
            {
                throw new ArgumentNullException(nameof(course));
            }

            Name = course.Name;
            WorkWidth = course.WorkWidth;
            NumHeadlandLanes = course.NumHeadlandLanes;
            HeadlandDirectionCW = course.HeadlandDirectionCW;
            Waypoints = course.Waypoints.Select(v => new Waypoint(this, v)).ToArray();
        }
    }
}
