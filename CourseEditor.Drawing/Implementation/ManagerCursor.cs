using System;
using System.Collections.Generic;
using System.Windows.Input;
using CourseEditor.Drawing.Contract;
using CursorType = CourseEditor.Drawing.Contract.CursorType;

namespace CourseEditor.Drawing.Implementation
{
    public class ManagerCursor : IManagerCursor
    {
        private readonly IDictionary<CursorType, Cursor> _cursors;
        private Action<Cursor> _setCursor;

        public ManagerCursor(IDictionary<CursorType, Cursor> cursors)
        {
            _cursors = cursors;
        }

        public void Init(Action<Cursor> setCursor)
        {
            _setCursor = setCursor;
        }

        public void SetCursor(CursorType arrow)
        {
            if (_setCursor == null)
            {
                return;
            }

            if (!_cursors.TryGetValue(arrow, out var cursorValue))
            {
                _setCursor(Cursors.Arrow);
                return;
            }
            _setCursor(cursorValue);
        }
    }
}
