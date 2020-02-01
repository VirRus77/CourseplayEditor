using System.Windows;
using System.Windows.Media;

namespace CourseplayEditor.Controls.Drawing
{
    public class PositionController
    {
        private Matrix _matrix;

        public PositionController(Matrix matrix)
        {
            _matrix = matrix;
        }

        public Point Position => new Point(_matrix.OffsetX, _matrix.OffsetY);
    }

    public class ScaleController
    {
        private readonly Matrix _matrix;

        public ScaleController(Matrix matrix)
        {
            _matrix = matrix;
        }

        public double Scale => _matrix.M11;
    }
}
