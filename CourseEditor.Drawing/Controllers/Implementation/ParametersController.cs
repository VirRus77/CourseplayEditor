using System;
using CourseEditor.Drawing.Controllers.Contract;
using SkiaSharp;

namespace CourseEditor.Drawing.Controllers.Implementation
{
    /// <inheritdoc />
    public class ParametersController : IParametersController
    {
        private readonly IMousePositionController _mousePositionController;
        private readonly IOffsetController _offsetController;
        private readonly IScaleController _scaleController;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mousePositionController"><inheritdoc cref="IMousePositionController"/></param>
        /// <param name="offsetController"><inheritdoc cref="IOffsetController"/></param>
        /// <param name="scaleController"><inheritdoc cref="IScaleController"/></param>
        public ParametersController(
            IMousePositionController mousePositionController,
            IOffsetController offsetController,
            IScaleController scaleController
        )      
        {
            _mousePositionController = mousePositionController;
            _offsetController = offsetController;
            _scaleController = scaleController;
        }

        public event EventHandler<EventArgs> Updated;

        /// <inheritdoc />
        public SKPoint PointLeftTop => _offsetController.PointLeftTop;

        /// <inheritdoc />
        public float Scale => _scaleController.Scale;

        protected void OnUpdated(SKPoint pointLeftTop, float scale)
        {
            Updated?.Invoke(this, new PositionScaleArgs(pointLeftTop, scale));
        }
    }
}
