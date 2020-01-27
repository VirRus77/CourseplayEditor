using System.IO;
using Courseplay.Tests.Tools;
using CourseplayEditor.Tools.Courseplay.v2019;
using CourseplayEditor.Tools.FarmSimulator;
using NUnit.Framework;

namespace Courseplay.Tests.v2019
{
    public class CourseManagerTests
    {
        private const string TestFileName = "courseManager.xml";

        [Test]
        public void ParseTest()
        {
            var serializer = CourseManager.Serializer;
            CourseManager courseManager;
            using (var stream = File.OpenRead(
                Path.Combine(TestDataHelper.GetTestDataPath(FarmSimulatorVersion.FarmingSimulator2019), TestFileName)
            ))
            {
                courseManager = serializer.Deserialize(stream) as CourseManager;
            }

            Assert.NotNull(courseManager, "courseManager != null");
            Assert.NotNull(courseManager.SaveSlots);
            Assert.AreEqual(courseManager.SaveSlots.Length, 4);
            AssertSaveSlot(
                courseManager.SaveSlots[0],
                new SaveSlot
                {
                    FileName = "courseStorage0001.xml",
                    Id = 1,
                    ParenId = 0,
                    IsUsed = true,
                    Name = "9-8.4",
                }
            );
        }

        private void AssertSaveSlot(SaveSlot read, SaveSlot answer)
        {
            Assert.AreEqual(read.FileName, answer.FileName);
            Assert.AreEqual(read.Id, answer.Id);
            Assert.AreEqual(read.IsUsed, answer.IsUsed);
            Assert.AreEqual(read.Name, answer.Name);
            Assert.AreEqual(read.ParenId, answer.ParenId);
        }
    }
}
