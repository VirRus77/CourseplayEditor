using System;
using System.Collections.Generic;
using System.Windows.Input;
using CourseEditor.Drawing.Contract;
using CourseEditor.Drawing.Controllers;
using CourseplayEditor.Contracts;
using CourseplayEditor.Implementation;
using CourseplayEditor.Tools;
using CourseplayEditor.Tools.Courseplay.v2019;
using CourseplayEditor.Tools.FarmSimulator;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Win32;

namespace CourseplayEditor.ViewModel
{
    public class MainWindowViewModel : ObservableObject
    {
        private readonly IDrawLayerManager _layerManager;
        private readonly ICourseFile _courseFile;
        private ICollection<Course> _courses;

        public MainWindowViewModel(IServiceProvider serviceProvider, IDrawLayerManager layerManager,ICourseFile courseFile)
        {
            ServiceProvider = serviceProvider;
            _layerManager = layerManager;
            _courseFile = courseFile;
            OpenCommand = new RelayCommand(() => OnRelayCommand());

            serviceProvider.GetService<MouseController>();
        }

        public IServiceProvider ServiceProvider { get; }

        public ICommand OpenCommand { get; }

        public ICollection<Course> Courses
        {
            get => _courses;
            set => SetValue(ref _courses, value);
        }

        private void OnRelayCommand()
        {
            var fileOpenDialog = new OpenFileDialog
            {
                InitialDirectory = GamePaths.GetSavesPath(FarmSimulatorVersion.FarmingSimulator2019),
                CheckFileExists = true,
                CheckPathExists = true,
                Multiselect = false,
            };

            if (fileOpenDialog.ShowDialog() != true)
            {
                return;
            }

            var fileName = fileOpenDialog.FileName;
            _courseFile.Open(fileName);
            Courses = _courseFile.Courses;
            _layerManager.AddLayer(new CourseDrawLayer(Courses));
        }
    }
}
