using System;

namespace CourseEditor.Drawing.Controllers.Mouse
{
    [Flags]
    public enum MouseEventType
    {
        Move = 0x01,
        Up = 0x02,
        Down = 0x04,
        Wheel = 0x08,
    }
}
