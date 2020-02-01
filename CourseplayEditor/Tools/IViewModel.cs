using System.ComponentModel;

namespace CourseplayEditor.Tools
{
    public interface IViewModel<T>
        where T : class, INotifyPropertyChanged
    {
        T ViewModel { get; set; }
    }
}
