using System;
using CourseEditor.Drawing.Contract;
using CourseEditor.Drawing.Tools;

namespace CourseEditor.Drawing.Implementation
{
    public class ValueController<T> : IValueController<T>
    {
        private bool _changing;
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

        public IDisposable BeginChanging()
        {
            if (_changing)
            {
                return DisposeAction.Empty();
            }

            _changing = true;
            return new DisposeAction(
                () =>
                {
                    _changing = false;
                    RaiseChanged();
                });
        }

        private void RaiseChanged()
        {
            if (_changing)
            {
                return;
            }

            Changed?.Invoke(this, new ValueEventArgs<T>(Value));
        }
    }
}
