using System;
using CourseEditor.Drawing.Tools;

namespace CourseEditor.Drawing.Contract
{
    /// <summary>
    /// Контроллер значения
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IValueController<T>
    {
        /// <summary>
        /// Значение изменилось
        /// </summary>
        event EventHandler<ValueEventArgs<T>> Changed;

        /// <summary>
        /// Текущее значение
        /// </summary>
        T Value { get; }

        /// <summary>
        /// Установить <inheritdoc cref="Value"/>
        /// </summary>
        /// <param name="value"><inheritdoc cref="Value"/></param>
        void SetValue(T value);

        /// <summary>
        /// Блокировать измененеия
        /// </summary>
        /// <returns></returns>
        IDisposable BeginChanging();
    }
}
