using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using CourseplayEditor.Tools.FarmSimulator;
using CourseplayEditor.Tools.FarmSimulator.v2019.Map;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using File = System.IO.File;

namespace Courseplay.Tests
{
    [TestClass]
    public class ExportShapePaths
    {
        private class ShapePath
        {
            public ShapePath(uint id, string name, ICollection<string> path)
            {
                Id = id;
                Name = name;
                Path = path;
            }

            public uint Id { get; }
            public string Name { get; }
            public ICollection<string> Path { get; }

            public override string ToString()
            {
                return $"{Id} \"{Name}\" {(string.Join("/", Path))}";
            }
        }

        private static string Output = @"G:\NickProd\Farming Simulator 19\Temp";

        [TestMethod]
        [DataRow(FarmSimulatorVersion.FarmingSimulator2019, "mapDE.i3d.shapes")]
        [DataRow(FarmSimulatorVersion.FarmingSimulator2019, "mapUS.i3d.shapes")]
        public void ExportShapePathTests(FarmSimulatorVersion version, string shapeFileName)
        {
            var gameMapPath = GamePaths.GetGameMapsPath(version);
            if (!Directory.Exists(gameMapPath))
            {
                var message = $"Game map path not found: \"{version}\".";
                Trace.WriteLine(message);
                Assert.Inconclusive(message);
            }

            var mapPath = Path.Combine(gameMapPath, shapeFileName);
            if (!File.Exists(mapPath))
            {
                var message = $"Map not found \"{version}\": \"{mapPath}\".";
                Trace.WriteLine(message);
                Assert.Inconclusive(message);
            }

            var xmlMapFilePath = mapPath.Replace(GameConstants.SchapesFileExtension, GameConstants.XmlMapFileExtension);
            var mapFile = MapFile.Load(xmlMapFilePath);
            var shapePaths = FindShapePath(mapFile)
                             .OrderBy(v => v.Id)
                             .ToArray();
            File.WriteAllLines(Path.Combine(Output, version.ToString(), "shapes.path"), shapePaths.Select(v => v.ToString()));
        }

        private ICollection<ShapePath> FindShapePath(MapFile mapFile)
        {
            return mapFile.Scene.TransformGroup
                          .SelectMany(v => FindShapePath(v, new string[0]))
                          .ToArray();
        }

        private IEnumerable<ShapePath> FindShapePath(TransformGroup transform, ICollection<string> parents)
        {
            var parentPath = parents.Concat(
                                        new[]
                                        {
                                            transform.Name
                                        }
                                    )
                                    .ToArray();
            foreach (var shape in transform.Shapes ?? new Shape[0])
            {
                yield return new ShapePath(shape.ShapeId, shape.Name, parentPath);
            }

            foreach (var shapePath in transform.TransformGroups?.SelectMany(v => FindShapePath(v, parentPath)) ?? new ShapePath[0])
            {
                yield return shapePath;
            }
        }
    }
}
