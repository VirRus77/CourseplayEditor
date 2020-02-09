using System.IO;
using CourseplayEditor.Tools.FarmSimulator;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Courseplay.Tests
{
    [TestClass]
    public class GameSavePathTests
    {
        [TestInitialize]
        public void Setup()
        {
        }

        [TestMethod]
        [DataRow(FarmSimulatorVersion.FarmingSimulator2015)]
        [DataRow(FarmSimulatorVersion.FarmingSimulator2017)]
        [DataRow(FarmSimulatorVersion.FarmingSimulator2019)]
        public void GetGameSavePathTest(FarmSimulatorVersion version)
        {
            var path = GamePaths.GetSavesPath(version);
            if (!Directory.Exists(path))
            {
                Assert.Inconclusive($"{FarmSimulatorVersion.FarmingSimulator2019} not exist.");
            }
            Assert.IsTrue(Directory.Exists(path));
        }
    }
}