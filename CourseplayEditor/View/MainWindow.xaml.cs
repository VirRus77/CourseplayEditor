using System.Windows;
using CourseplayEditor.Contracts;
using CourseplayEditor.Tools;
using CourseplayEditor.ViewModel;

namespace CourseplayEditor.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IViewModel<MainWindowViewModel>, IMainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public MainWindow(MainWindowViewModel viewModel)
            : this()
        {
            ViewModel = viewModel;
        }

        public MainWindowViewModel ViewModel
        {
            get => (MainWindowViewModel) DataContext;
            set => DataContext = value;
        }
    }
}
