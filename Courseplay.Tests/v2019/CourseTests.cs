using System;
using System.IO;
using System.Xml;
using Courseplay.Tests.Tools;
using CourseplayEditor.Tools.Courseplay.Data;
using CourseplayEditor.Tools.Courseplay.v2019;
using CourseplayEditor.Tools.FarmSimulator;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Courseplay.Tests.v2019
{
    [TestClass]
    public class CourseTests
    {
        private const string TestFileName = "courseStorage0003.xml";

        [TestMethod]
        public void ParseTest()
        {
            var serializer = Course.Serializer;
            Course course;

            var waypoints = new XmlDocument();
            using var stream = File.OpenRead(
                Path.Combine(TestDataHelper.GetTestDataPath(FarmSimulatorVersion.FarmingSimulator2019), TestFileName)
            );
            waypoints.Load(stream);

            try
            {
                course = serializer.Deserialize(WaypointHelper.FromCoursePlay(waypoints)) as Course;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            Assert.IsNotNull(course, "course != null");
            Assert.IsNotNull(course.Waypoints);
            Assert.AreEqual(course.Waypoints.Length, 77);
            AssertWaypoint(
                course.Waypoints[14],
                new Waypoint
                {
                    // <waypoint15 turnstart="1" speed="0" angle="95.55" pos="-442.40 84.00 733.64"/>
                    TurnStart = 1,
                    Speed = 0,
                    Angle = 95.55f,
                    PointX = -442.40f,
                    PointY = 84.00f,
                    PointZ = 733.64f,
                    Position = "-442.40 84.00 733.64"
                }
            );
            AssertWaypoint(
                course.Waypoints[15],
                new Waypoint
                {
                    // <waypoint16 speed="0" angle="31.06" pos="-433.81 84.00 732.81" turnend="1"/>
                    TurnEnd = 1,
                    Speed = 0,
                    Angle = 31.06f,
                    PointX = -433.81f,
                    PointY = 84.00f,
                    PointZ = 732.81f,
                    Position = "-433.81 84.00 732.81"
                }
            );
            AssertWaypoint(
                course.Waypoints[33],
                new Waypoint
                {
                    //<waypoint34 speed="0" angle="-78.93" pos="-456.28 83.99 751.61" wait="1"/>
                    Wait = 1,
                    Speed = 0,
                    Angle = -78.93f,
                    PointX = -456.28f,
                    PointY = 83.99f,
                    PointZ = 751.61f,
                    Position = "-456.28 83.99 751.61"
                }
            );
            AssertWaypoint(
                course.Waypoints[39],
                new Waypoint
                {
                    // <waypoint40 speed="0" angle="-86.61" unload="1" pos="-483.67 80.66 752.96"/>
                    Unload = 1,
                    Speed = 0,
                    Angle = -86.61f,
                    PointX = -483.67f,
                    PointY = 80.66f,
                    PointZ = 752.96f,
                    Position = "-483.67 80.66 752.96"
                }
            );
            AssertWaypoint(
                course.Waypoints[46],
                new Waypoint
                {
                    // <waypoint47 speed="22" angle="111.01" rev="1" pos="-499.55 78.93 749.08"/>
                    Reverse = 1,
                    Speed = 22,
                    Angle = 111.01f,
                    PointX = -499.55f,
                    PointY = 78.93f,
                    PointZ = 749.08f,
                    Position = "-499.55 78.93 749.08"
                }
            );
            AssertWaypoint(
                course.Waypoints[76],
                new Waypoint
                {
                    // <waypoint77 speed="36" angle="-178.15" pos="-577.84 77.87 711.02" crossing="1"/>
                    Crossing = 1,
                    Speed = 36,
                    Angle = -178.15f,
                    PointX = -577.84f,
                    PointY = 77.87f,
                    PointZ = 711.02f,
                    Position = "-577.84 77.87 711.02"
                }
            );
        }

        private void AssertWaypoint(Waypoint read, Waypoint answer)
        {
            Assert.AreEqual(read.Angle, answer.Angle);
            Assert.AreEqual(read.Crossing, answer.Crossing);
            Assert.AreEqual(read.PointX, answer.PointX);
            Assert.AreEqual(read.PointY, answer.PointY);
            Assert.AreEqual(read.PointZ, answer.PointZ);
            Assert.AreEqual(read.Position, answer.Position);
            Assert.AreEqual(read.Speed, answer.Speed);
            Assert.AreEqual(read.TurnStart, answer.TurnStart);
            Assert.AreEqual(read.TurnEnd, answer.TurnEnd);
            Assert.AreEqual(read.Unload, answer.Unload);
            Assert.AreEqual(read.Wait, answer.Wait);
        }
    }
}
