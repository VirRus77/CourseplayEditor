using System.Collections.Generic;
using CourseplayEditor.Tools.Courseplay.v2019;

namespace CourseplayEditor.Contracts
{
    public interface ICourseFile
    {
        void Open(string fileName);
        ICollection<Course> Courses { get; }
    }
}
