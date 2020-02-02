using System;

namespace CourseplayEditor.Model
{
    public abstract class BaseModel
    {
        /// <summary>
        /// Событие при изменении <inheritdoc cref="BaseModel"/>
        /// </summary>
        public event EventHandler<EventArgs> Changed;

        /// <summary>
        /// Вызвать <see cref="Changed"/>
        /// </summary>
        protected void RaiseChanged()
        {
            Changed?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="newValue"></param>
        /// <typeparam name="T"></typeparam>
        protected void SetValue<T>(ref T value, in T newValue)
        {
            if (Equals(value, newValue))
            {
                return;
            }

            value = newValue;
            RaiseChanged();
        }
    }
}
