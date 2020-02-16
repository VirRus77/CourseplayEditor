using System;

namespace DevelopCursor.Tests.Tools
{
    public interface IDisposableValue<T> : IDisposable
    {
        T Value { get; }
    }

    public abstract class DisposableValue<T> : IDisposableValue<T>
    {
        protected DisposableValue(T value)
        {
            Value = value;
        }

        public T Value { get; private set; }

        public abstract void Dispose();
    }

    public class DisposableValueAction<T> : DisposableValue<T>
    {
        private readonly Action<T> _onDispose;

        public DisposableValueAction(T value, Action<T> onDispose)
            : base(value)
        {
            _onDispose = onDispose;
        }

        public override void Dispose()
        {
            _onDispose(Value);
        }
    }
}
