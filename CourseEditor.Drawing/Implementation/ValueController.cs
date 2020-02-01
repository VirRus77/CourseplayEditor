using System;
using CourseEditor.Drawing.Contract;
using CourseEditor.Drawing.Tools;

namespace CourseEditor.Drawing.Implementation
{
    public class ValueController<T> : IValueController<T>
    {
        public event EventHandler<ValueEventArgs<T>> Changed;

        protected ValueController(T value = default)
        {
            Value = value;
        }

        public T Value { get; private set; }

        public void SetValue(T value)
        {
            Value = value;
            RaiseChanged();
        }

        private void RaiseChanged()
        {
            Changed?.Invoke(this, new ValueEventArgs<T>(Value));
        }
    }
}
