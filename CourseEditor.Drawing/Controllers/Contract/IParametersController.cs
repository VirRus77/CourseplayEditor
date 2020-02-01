using System;
using SkiaSharp;

namespace CourseEditor.Drawing.Controllers.Contract
{
    public interface IParametersController
    {
        event EventHandler<EventArgs> Updated;

        /// <inheritdoc cref="IOffsetController.PointLeftTop"/>
        SKPoint PointLeftTop { get; }

        /// <inheritdoc cref="IScaleController.Scale"/>
        float Scale { get; }
    }
}
