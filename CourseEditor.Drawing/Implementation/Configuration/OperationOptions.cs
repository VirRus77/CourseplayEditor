using CourseEditor.Drawing.Contract.Configuration;

namespace CourseEditor.Drawing.Implementation.Configuration
{
    /// <inheritdoc />
    public class OperationOptions : IOperationOptions
    {
        /// <inheritdoc />
        public float ScaleFactor { get; set; } = 1.25f;

        /// <inheritdoc />
        public int SelectRadius { get; set; } = 5;

        /// <inheritdoc />
        public int RectangleToleranceRadius { get; set; } = 3;

        /// <inheritdoc />
        public int MoveToleranceRadius { get; } = 3;
    }
}
