using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Input;
using Core.Tools.Extensions;
using CourseEditor.Drawing.Controllers;
using CourseplayEditor.Contracts;
using CourseplayEditor.Implementation;
using CourseplayEditor.Tools;
using CourseplayEditor.Tools.Courseplay.v2019;
using CourseplayEditor.Tools.FarmSimulator;
using CourseplayEditor.Tools.FarmSimulator.v2019.Map;
using I3DShapesTool.Lib.Container;
using I3DShapesTool.Lib.Model;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Win32;
using File = System.IO.File;

namespace CourseplayEditor.ViewModel
{
    public class MainWindowViewModel : ObservableObject
    {
        private readonly ICourseLayerManager _layerManager;
        private BackgroundMapDrawLayer _mapBackground;
        private readonly ICourseFile _courseFile;
        private ICollection<Course> _courses;

        public MainWindowViewModel(IServiceProvider serviceProvider, ICourseLayerManager layerManager, ICourseFile courseFile)
        {
            ServiceProvider = serviceProvider;
            _layerManager = layerManager;
            _courseFile = courseFile;

            OpenCommand = new RelayCommand(() => OpenCommandExecute());
            OpenMapCommand = new RelayCommand(() => OpenMapCommandExecute());
            AddMapSplinesCommand = new RelayCommand(() => AddMapSplinesCommandExecute());

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

        private void OpenCommandExecute()
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
            _layerManager.AddCourses(Courses);
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
                Filter = "Pda MAP DDS File|pda_map_H.dds.dds",
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
                Filter = "MAP Files|*.i3d.shapes",
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

            var fileContainer = new FileContainer(filePath);
            var entities = fileContainer.GetEntities()
                                        .Where(entity => entity.Type == 2);
            var splines = fileContainer.ReadRawData(entities)
                                       .Select(v => new Spline(v.RawData, fileContainer.Endian, fileContainer.Header.Version))
                                       .ToArray();

            splines = TransformToMap(filePath, splines) ?? splines;
            _layerManager.AddMapSplines(splines);
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
