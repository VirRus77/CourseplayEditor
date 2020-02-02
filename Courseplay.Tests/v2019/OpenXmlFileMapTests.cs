using System.IO;
using CourseplayEditor.Tools.FarmSimulator;
using CourseplayEditor.Tools.FarmSimulator.v2019.Map;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using File = System.IO.File;

namespace Courseplay.Tests.v2019
{
    [TestClass]
    public class OpenXmlFileMapTests
    {
        [TestMethod]
        [DataRow(FarmSimulatorVersion.FarmingSimulator2019, "mapDE.i3d")]
        [DataRow(FarmSimulatorVersion.FarmingSimulator2019, "mapUS.i3d")]
        public void OpenXmlFileMapTest(FarmSimulatorVersion version, string fileName)
        {
            var mapsPath = GamePaths.GetGameMapsPath(version);
            if (mapsPath == null)
            {
                Assert.Inconclusive("Maps path by \"{0}\" not found.", version);
            }

            var mapFilePath = Path.Combine(mapsPath, fileName);
            if (!File.Exists(mapFilePath))
            {
                Assert.Inconclusive("Map file \"{0}\" by \"{1}\" not found.", fileName, version);
            }

            using (var stream = File.OpenRead(mapFilePath))
            {
                var mapFile = MapFile.Serializer.Deserialize(stream) as MapFile;
                Assert.IsNotNull(mapFile);
            }
        }
    }
}
