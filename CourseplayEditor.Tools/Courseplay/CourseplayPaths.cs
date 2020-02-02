using System.IO;
using CourseplayEditor.Tools.FarmSimulator;

namespace CourseplayEditor.Tools.Courseplay
{
    public static class CourseplayPaths
    {
        public static string GetCourseplayDirectory(FarmSimulatorVersion version)
        {
            var gameDirectory = GamePaths.GetSavesPath(version);
            if (gameDirectory == null)
            {
                return null;
            }
            var coursePlayDirectory = Path.Combine(gameDirectory, CourseplayConstants.CourseplayDirectory);
            if (!Directory.Exists(coursePlayDirectory))
            {
                return null;
            }
            return coursePlayDirectory;
        }
    }
}
