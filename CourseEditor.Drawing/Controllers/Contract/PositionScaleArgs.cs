using System;
using SkiaSharp;

namespace CourseEditor.Drawing.Controllers.Contract
{
    public class PositionScaleArgs : EventArgs 
    {
        public PositionScaleArgs(SKPoint pointLeftTop, double scale)
        {
            PointLeftTop = pointLeftTop;
            Scale = scale;
        }
        
        /// <inheritdoc cref="IOffsetController.PointLeftTop"/>
        public SKPoint PointLeftTop { get; }

        /// <inheritdoc cref="IScaleController.Scale"/>
        public double Scale { get; }
    }
}
