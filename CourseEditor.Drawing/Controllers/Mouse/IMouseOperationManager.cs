using System;
using System.Windows;
using System.Windows.Input;
using CourseEditor.Drawing.Contract.Operations;

namespace CourseEditor.Drawing.Controllers.Mouse
{
    /// <summary>
    /// 
    /// </summary>
    public interface IMouseOperationManager : IMouseEvents
    {
        /// <summary>
        /// Текущая операция мишки.
        /// </summary>
        IOperation CurrentOperation { get; }

        void StopOperation();
        void KeyChanged(KeyEventArgs keyEventArgs);
    }
}
