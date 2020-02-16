using System;
using System.Windows;
using CourseEditor.Drawing.Controllers;
using CourseplayEditor.Contracts;
using Microsoft.Extensions.DependencyInjection;

namespace CourseplayEditor
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static ApplicationDependencyInjection _dependencyInjection;
        private IServiceProvider ServiceProvider => _dependencyInjection.ServiceProvider;

        public App()
        {
            _dependencyInjection = new ApplicationDependencyInjection();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            var mainWindows = ServiceProvider.GetService<IMainWindow>();
            var mouseController = ServiceProvider.GetService<MouseController>();
            MainWindow = mainWindows as Window;
            if (MainWindow == null)
            {
                throw new Exception($"Instance {nameof(IMainWindow)} is now {nameof(Windows)} class.");
            }
            mouseController.InitKeyboardHook(MainWindow);
            MainWindow.Show();
        }
    }
}
