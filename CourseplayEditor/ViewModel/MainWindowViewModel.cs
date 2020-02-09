using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Input;
using Core.Tools.Extensions;
using CourseEditor.Drawing.Contract;
using CourseEditor.Drawing.Controllers;
using CourseEditor.Drawing.Implementation;
using CourseplayEditor.Contracts;
using CourseplayEditor.Implementation;
using CourseplayEditor.Model;
using CourseplayEditor.Tools;
using CourseplayEditor.Tools.Courseplay;
using CourseplayEditor.Tools.FarmSimulator;
using CourseplayEditor.Tools.FarmSimulator.v2019.Map;
using I3dShapes.Container;
using I3dShapes.Model;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Win32;
using File = System.IO.File;

namespace CourseplayEditor.ViewModel
{
    public class MainWindowViewModel : ObservableObject
    {
        private readonly ICourseLayerManager _layerManager;
        private readonly ICourseFile _courseFile;
        private readonly SelectableObjects.IModelSelectableObjects _selectableObjects;
        private readonly ISelectableController _selectableController;
        private ICollection<Course> _courses;
        private ICollection<SplineMap> _splineMaps;

        public MainWindowViewModel(
            IServiceProvider serviceProvider,
            ICourseLayerManager layerManager,
            ICourseFile courseFile,
            SelectableObjects.IModelSelectableObjects selectableObjects,
            ISelectableController selectableController
        )
        {
            ServiceProvider = serviceProvider;
            _layerManager = layerManager;
            _courseFile = courseFile;
            _selectableObjects = selectableObjects;
            _selectableController = selectableController;

            OpenCommand = new RelayCommand(() => OpenCommandExecute());
            OpenMapCommand = new RelayCommand(() => OpenMapCommandExecute());
            AddMapSplinesCommand = new RelayCommand(() => AddMapSplinesCommandExecute());

            // Manual
            serviceProvider.GetService<MouseController>();
        }

        public IServiceProvider ServiceProvider { get; }

        public ICommand OpenCommand { get; }

        public ICommand OpenMapCommand { get; }

        public ICommand AddMapSplinesCommand { get; }

        public ICollection<Course> Courses
        {
            get => _courses;
            set => SetValue(ref _courses, value);
        }

        public ISelectable SelectedItem
        {
            get => _selectableController.Value.FirstOrDefault();
            set => _selectableController.Select(value);
        }

        public ICollection<SplineMap> SplineMaps
        {
            get => _splineMaps;
            set => SetValue(ref _splineMaps, value);
        }

        private void OpenCommandExecute()
        {
            var fileOpenDialog = new OpenFileDialog
            {
                InitialDirectory = CourseplayPaths.GetCourseplayDirectory(FarmSimulatorVersion.FarmingSimulator2019),
                CheckFileExists = true,
                CheckPathExists = true,
                Multiselect = false,
                Filter = $"Courseplay Manager|{CourseplayConstants.CourseplayFileName}|Xml|*.xml"
            };
            
            if (fileOpenDialog.ShowDialog() != true)
            {
                return;
            }

            var fileName = fileOpenDialog.FileName;
            _courseFile.Open(fileName);
            Courses = _courseFile.Courses
                                 .Select(course => new Course(course))
                                 .ToArray();
            _layerManager.AddCourses(Courses);
            _selectableObjects.AddSelectableObjects(Courses);
            AddMapBackgroundByCourseFile(fileName);
            AddMapSplinesByCourseFile(fileName);
        }

        private void OpenMapCommandExecute()
        {
            var fileOpenDialog = new OpenFileDialog
            {
                InitialDirectory = GamePaths.GetGameMapsPath(FarmSimulatorVersion.FarmingSimulator2019),
                CheckFileExists = true,
                CheckPathExists = true,
                Multiselect = false,
                Filter = $"Pda MAP DDS File|{GameConstants.MapPdaFileName}",
            };

            if (fileOpenDialog.ShowDialog() != true)
            {
                return;
            }

            var fileName = fileOpenDialog.FileName;

            AddMapBackground(fileName);
        }

        private void AddMapSplinesCommandExecute()
        {
            var fileOpenDialog = new OpenFileDialog
            {
                InitialDirectory = GamePaths.GetGameMapsPath(FarmSimulatorVersion.FarmingSimulator2019),
                CheckFileExists = true,
                CheckPathExists = true,
                Multiselect = false,
                Filter = $"MAP Files|*{GameConstants.SchapesFileExtension}",
            };

            if (fileOpenDialog.ShowDialog() != true)
            {
                return;
            }

            var fileName = fileOpenDialog.FileName;
            AddMapSplines(fileName);
        }

        private void AddMapBackground(string fileName)
        {
            if (!File.Exists(fileName))
            {
                return;
            }

            _layerManager.AddBackgroundMap(fileName);
        }

        private void AddMapSplines(string filePath)
        {
            if (!File.Exists(filePath))
            {
                return;
            }

            var fileContainer = new ShapeFile(filePath);
            var splines = fileContainer.ReadKnowTypes(ShapeType.Spline)
                                       .OfType<Spline>()
                                       .ToArray();

            splines = TransformToMap(filePath, splines) ?? splines;
            SplineMaps = splines.Select(v => new SplineMap(v)).ToArray();
            _selectableObjects.AddSelectableObjects(SplineMaps);
            _layerManager.AddMapSplines(SplineMaps);
        }


        private void AddMapBackgroundByCourseFile(string fileName)
        {
            var directory = new DirectoryInfo(Path.GetDirectoryName(fileName));
            var mapName = directory.Name;
            var mapsPath = GamePaths.GetGameMapsPath(FarmSimulatorVersion.FarmingSimulator2019);
            if (mapsPath == null)
            {
                return;
            }

            var mapPdaPath = Path.Combine(mapsPath, mapName, GameConstants.MapPdaFileName);
            if (!File.Exists(mapPdaPath))
            {
                return;
            }

            AddMapBackground(mapPdaPath);
        }

        private void AddMapSplinesByCourseFile(string fileName)
        {
            var directory = new DirectoryInfo(Path.GetDirectoryName(fileName));
            var mapName = directory.Name;
            var mapsPath = GamePaths.GetGameMapsPath(FarmSimulatorVersion.FarmingSimulator2019);
            if (mapsPath == null)
            {
                return;
            }

            var mapFilePath = Path.Combine(mapsPath, $"{mapName}{GameConstants.SchapesFileExtension}");
            if (!File.Exists(mapFilePath))
            {
                return;
            }

            AddMapSplines(mapFilePath);
        }

        private Spline[] TransformToMap(string filePath, Spline[] splines)
        {
            var xmlFileName = Path.GetFileName(filePath)
                                  .Replace(
                                      GameConstants.SchapesFileExtension,
                                      GameConstants.XmlMapFileExtension,
                                      StringComparison.InvariantCultureIgnoreCase
                                  );
            var xmlFilePath = Path.Combine(Path.GetDirectoryName(filePath), xmlFileName);
            if (!File.Exists(xmlFilePath))
            {
                return null;
            }

            var fileMap = MapFile.Load(xmlFilePath);
            var ids = splines.Select(v => v.Id).ToArray();
            var shapesDic = FindTransformations(fileMap, ids);
            splines.ForEach(
                v =>
                {
                    if (!shapesDic.TryGetValue(v.Id, out var shapes))
                    {
                        return;
                    }

                    var shape = shapes.Single();
                    var transformation = shape.TranslationValue;
                    if (transformation == null)
                    {
                        return;
                    }

                    v.Points.ForEach(
                        v =>
                        {
                            v.X += transformation[0];
                            v.Y += transformation[1];
                            v.Z += transformation[2];
                        }
                    );
                }
            );
            return null;
        }

        private IDictionary<uint, Shape[]> FindTransformations(MapFile fileMap, uint[] shapeIds)
        {
            var values = FindShapes(fileMap.Scene.TransformGroup, shapeIds);
            return values;
        }

        private IDictionary<uint, Shape[]> FindShapes(TransformGroup[] sceneTransformGroup, uint[] shapeIds)
        {
            var shapes = sceneTransformGroup
                         .Descendants(transformGroup => transformGroup.TransformGroups)
                         .SelectMany(
                             transformGroup => transformGroup.Shapes?.Where(shape => shapeIds.Contains(shape.ShapeId)) ?? new Shape[0]
                         )
                         .Where(shape => shape != null)
                         .GroupBy(shape => shape.ShapeId)
                         .ToDictionary(v => v.Key, v => v.ToArray());
            return shapes;
        }
    }
}
