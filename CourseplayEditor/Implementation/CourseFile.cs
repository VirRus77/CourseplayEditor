using System.Collections.Generic;
using System.IO;
using System.Linq;
using CourseplayEditor.Contracts;
using CourseplayEditor.Tools.Courseplay.Data;
using CourseplayEditor.Tools.Courseplay.v2019;

namespace CourseplayEditor.Implementation
{
    public class CourseFile : ICourseFile
    {
        public CourseFile() { }

        public void Open(string fileName)
        {
            var courseManager = OpenCourseManager(fileName);
            var path = Path.GetDirectoryName(fileName);
            Courses = courseManager
                      .SaveSlots
                      .Select(v => OpenCourse(Path.Combine(path, v.FileName)))
                      .ToArray();
        }

        private CourseManager OpenCourseManager(string fileName)
        {
            using var stream = File.OpenRead(fileName);
            return CourseManager.Serializer.Deserialize(stream) as CourseManager;
        }

        private Course OpenCourse(string fileName)
        {
            using var stream = File.OpenRead(fileName);
            using var courseStream = WaypointHelper.FromCoursePlay(stream);
            return Course.Serializer.Deserialize(courseStream) as Course;
        }

        public ICollection<Course> Courses { get; private set; }
    }
}
