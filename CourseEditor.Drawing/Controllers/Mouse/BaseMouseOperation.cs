using System.Windows.Input;
using SkiaSharp;

namespace CourseEditor.Drawing.Controllers.Mouse
{
    public abstract class BaseMouseOperation : IMouseOperation
    {
        public BaseMouseOperation(MouseOperationType mouseOperation, MouseEventType mouseEventType)
        {
            OperationType = mouseOperation;
            SupportMouseEvent = mouseEventType;
        }

        /// <inheritdoc />
        public bool IsRun { get; protected set; }

        /// <inheritdoc />
        public MouseOperationType OperationType { get; }

        /// <inheritdoc />
        public MouseEventType SupportMouseEvent { get; }

        public virtual void Stop()
        {
            IsRun = false;
        }

        /// <summary>
        /// Точка на чала операции.
        /// </summary>
        public SKPoint StartPoint { get; private set; }

        /// <summary>
        /// Последняя точка события мышки.
        /// </summary>
        public SKPoint LastDeltaPoint { get; protected set; }

        /// <inheritdoc />
        public virtual bool OnMouseMove(MouseEventArgs mouseEventArgs, SKPoint controlPosition)
        {
            throw new System.NotImplementedException();
        }

        /// <inheritdoc />
        public virtual bool OnMouseUp(MouseButtonEventArgs mouseButtonEventArgs, SKPoint controlPosition)
        {
            throw new System.NotImplementedException();
        }

        /// <inheritdoc />
        public virtual bool OnMouseDown(MouseButtonEventArgs mouseButtonEventArgs, SKPoint controlPosition)
        {
            throw new System.NotImplementedException();
        }

        /// <inheritdoc />
        public virtual bool OnMouseWheel(MouseWheelEventArgs mouseWheelEventArgs, SKPoint controlPosition)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Начало операции.
        /// </summary>
        /// <param name="currentPoint"></param>
        protected virtual void Start(SKPoint currentPoint)
        {
            StartPoint = currentPoint;
            LastDeltaPoint = StartPoint;
            IsRun = true;
        }

        /// <summary>
        /// Окончание операции.
        /// </summary>
        /// <param name="currentPoint"></param>
        protected virtual void End(SKPoint currentPoint)
        {
            LastDeltaPoint = currentPoint;
            IsRun = false;
        }
    }
}
