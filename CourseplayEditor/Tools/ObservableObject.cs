using System.ComponentModel;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;

namespace CourseplayEditor.Tools
{
    /// <summary>
    /// Класс для обёртывания работы с INotifyPropertyChanged
    /// </summary>
    public class ObservableObject : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void SetValue<T>(ref T value, T newValue, [CallerMemberName] string propertyName = null)
        {
            if (Equals(value, newValue))
            {
                return;
            }

            value = newValue;
            OnPropertyChanged(propertyName);
        }

        /// <summary>
        /// Дергаем событие по изменению свойства.
        /// </summary>
        /// <param name="propertyName">Имя свойства.</param>
        [NotifyPropertyChangedInvocator]
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            handler?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
