using System;
using CourseEditor.Drawing.Controllers.Contract;

namespace CourseEditor.Drawing.Controllers.Implementation
{
    /// <inheritdoc />
    public class ScaleController : IScaleController
    {
        public event EventHandler<EventArgs> Updated;

        /// <inheritdoc />
        public float Scale { get; } = 1f;

        protected void OnUpdated()
        {
            Updated?.Invoke(this, EventArgs.Empty);
        }
    }
}
