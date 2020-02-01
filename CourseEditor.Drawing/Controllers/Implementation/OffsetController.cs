using System;
using CourseEditor.Drawing.Controllers.Contract;
using SkiaSharp;

namespace CourseEditor.Drawing.Controllers.Implementation
{
    /// <inheritdoc />
    public class OffsetController : IOffsetController
    {
        public event EventHandler<EventArgs> Updated;

        /// <inheritdoc />
        public SKPoint PointLeftTop { get; } = SKPoint.Empty;

        protected void OnUpdated()
        {
            Updated?.Invoke(this, EventArgs.Empty);
        }
    }
}
