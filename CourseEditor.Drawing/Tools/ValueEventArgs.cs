using System;

namespace CourseEditor.Drawing.Tools
{
    public class ValueEventArgs<T> : EventArgs
    {
        public ValueEventArgs(T value)
        {
            Value = value;
        }

        public T Value { get; }
    }
}
