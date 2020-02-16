using System.Linq;
using System.Windows.Input;
using CourseEditor.Drawing.Contract;
using SkiaSharp;

namespace CourseEditor.Drawing.Controllers.Mouse
{
    /// <summary>
    /// Move selected objects.
    /// </summary>
    public class MouseOperationMove : BaseMouseOperation
    {
        private readonly ISelectableController _selectableController;

        public MouseOperationMove(ISelectableController selectableController)
            : base(MouseOperationType.Move, MouseEventType.Move | MouseEventType.Down)
        {
            _selectableController = selectableController;
        }

        //public override bool OnMouseMove(MouseEventArgs mouseEventArgs, SKPoint controlPosition)
        //{
        //    if (_selectableController?.Value.Any() != true)
        //    {
        //        return false;
        //    }
        //}
    }
}
