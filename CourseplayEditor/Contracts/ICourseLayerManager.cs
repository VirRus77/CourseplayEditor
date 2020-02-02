using System.Collections.Generic;
using CourseplayEditor.Tools.Courseplay.v2019;
using I3DShapesTool.Lib.Model;

namespace CourseplayEditor.Contracts
{
    public interface ICourseLayerManager
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="courses"></param>
        void AddCourses(in ICollection<Course> courses);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileName"></param>
        void AddBackgroundMap(in string fileName);

        void AddMapSplines(in IEnumerable<Spline> splines);
    }
}
