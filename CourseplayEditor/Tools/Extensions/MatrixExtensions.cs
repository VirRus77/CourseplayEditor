using System.Windows.Media;

namespace CourseplayEditor.Tools.Extensions
{
    public static class MatrixExtensions
    {
        public static double Stretches(this Matrix matrix) => matrix.M11;
    }
}
