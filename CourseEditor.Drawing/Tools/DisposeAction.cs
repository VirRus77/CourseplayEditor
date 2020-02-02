using System;
using System.Diagnostics.CodeAnalysis;

namespace CourseEditor.Drawing.Tools
{
    /// <summary>
    /// 
    /// </summary>
    public class DisposeAction : IDisposable
    {
        private readonly Action _action;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="action">Метод выполниющийся после уничтожение объекта</param>
        public DisposeAction([NotNull] in Action action)
        {
            _action = action ?? throw new ArgumentNullException(nameof(action));
        }

        public static DisposeAction Empty() => new DisposeAction(() => { });

        public void Dispose()
        {
            _action?.Invoke();
        }
    }
}
