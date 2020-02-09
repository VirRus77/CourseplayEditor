namespace CourseEditor.Drawing.Controllers.Mouse
{
    public interface IMouseOperation : IMouseEvents
    {
        bool IsRun { get; }

        MouseOperationType OperationType { get; }

        MouseEventType SupportMouseEvent { get; }
    }
}
