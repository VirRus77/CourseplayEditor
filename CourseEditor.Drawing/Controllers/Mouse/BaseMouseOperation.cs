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

        public bool IsRun { get; protected set; }

        public MouseOperationType OperationType { get; }

        public MouseEventType SupportMouseEvent { get; }

        public SKPoint StartPoint { get; private set; }

        public SKPoint LastDeltaPoint { get; protected set; }

        public virtual bool OnMouseMove(MouseEventArgs mouseEventArgs, SKPoint controlPosition)
        {
            throw new System.NotImplementedException();
        }

        public virtual bool OnMouseUp(MouseButtonEventArgs mouseButtonEventArgs, SKPoint controlPosition)
        {
            throw new System.NotImplementedException();
        }

        public virtual bool OnMouseDown(MouseButtonEventArgs mouseButtonEventArgs, SKPoint controlPosition)
        {
            throw new System.NotImplementedException();
        }

        public virtual bool OnMouseWheel(MouseWheelEventArgs mouseWheelEventArgs, SKPoint controlPosition)
        {
            throw new System.NotImplementedException();
        }

        protected virtual void Start(SKPoint currentPoint)
        {
            StartPoint = currentPoint;
            LastDeltaPoint = StartPoint;
            IsRun = true;
        }

        protected virtual void End(SKPoint currentPoint)
        {
            LastDeltaPoint = currentPoint;
            IsRun = false;
        }
    }
}
