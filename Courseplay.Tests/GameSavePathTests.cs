using System.IO;
using CourseplayEditor.Tools.FarmSimulator;
using NUnit.Framework;

namespace Courseplay.Tests
{
    public class GameSavePathTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void GetGameSavePathTest([Values]FarmSimulatorVersion version)
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