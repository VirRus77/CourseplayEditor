using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using SkiaSharp;

namespace CourseEditor.Drawing.Controllers.Mouse
{
    public class CaptureMouseOperation : BaseMouseOperation
    {
        public CaptureMouseOperation(MouseOperationType mouseOperation, MouseEventType mouseEventType)
            : base(mouseOperation, mouseEventType)
        {
        }

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
    }
}
