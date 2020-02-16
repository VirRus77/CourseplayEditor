using System;
using System.Windows.Input;

namespace CourseEditor.Drawing.Contract
{
    public interface IManagerCursor
    {
        void SetCursor(CursorType arrow);
        void Init(Action<Cursor> setCursor);
    }
}
