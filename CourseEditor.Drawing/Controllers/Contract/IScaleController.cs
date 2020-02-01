using System;

namespace CourseEditor.Drawing.Controllers.Contract
{
    /// <summary>
    /// Управление масштабом
    /// </summary>
    public interface IScaleController : IController
    {
        event EventHandler<EventArgs> Updated;

        /// <summary>
        /// Масштаб
        /// </summary>
        float Scale { get; }
    }
}
