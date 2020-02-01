using CourseEditor.Drawing.Contract;
using SkiaSharp;

namespace CourseEditor.Drawing.Implementation
{
    /// <inheritdoc cref="ICurrentPositionController"/>
    public class CurrentPositionController : ValueController<SKPoint>, ICurrentPositionController
    {
    }
}
