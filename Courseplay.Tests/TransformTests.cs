using System.Collections.Generic;
using System.Linq;
using CourseEditor.Drawing.Controllers.Implementation;
using CourseEditor.Drawing.Tools;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SkiaSharp;

namespace Courseplay.Tests
{
    [TestClass]
    public class TransformTests
    {
        private readonly static ICollection<(MapSettings MapSettings, SKPoint ControlPoint, SKPoint Answer)> TestToMapPointData = new[]
        {
            (new MapSettings(new SKPoint(0, 0), 1f), new SKPoint(50, 100), new SKPoint(50, 100)),
            (new MapSettings(new SKPoint(50, 25), 1f), new SKPoint(50, 100), new SKPoint(100, 125)),
            (new MapSettings(new SKPoint(-50, -25), 1f), new SKPoint(50, 100), new SKPoint(0, 75)),
            (new MapSettings(new SKPoint(-50, -25), 1f), new SKPoint(10, 20), new SKPoint(10 / 1 + -50, 20 / 1 + -25)),
            (new MapSettings(new SKPoint(-50, -25), 2f), new SKPoint(50, 100), new SKPoint(50 / 2 + -50, 100 / 2 + -25)),
        };

        private readonly static ICollection<(MapSettings MapSettings, SKPoint ControlPoint, SKPoint Answer)> TestToControlPointData = new[]
        {
            (new MapSettings(new SKPoint(0, 0), 1f), new SKPoint(50, 100), new SKPoint(50, 100)),
            (new MapSettings(new SKPoint(50, 25), 1f), new SKPoint(50, 100), new SKPoint(0, 75)),
            (new MapSettings(new SKPoint(-50, -25), 1f), new SKPoint(50, 100), new SKPoint(100, 125)),
            (new MapSettings(new SKPoint(-50, -25), 1f), new SKPoint(10, 20), new SKPoint(10 - -50, 20 - -25)),
            (new MapSettings(new SKPoint(-50, -25), 2f), new SKPoint(50, 100), new SKPoint((50 - -50) / 2f, (100 - -25) / 2f)),
        };

        [TestMethod]
        public void TestToMap()
        {
            TestToMapPointData
                .ToList()
                .ForEach(v => AssertTestDataToMap(v));
        }

        [TestMethod]
        public void TestToControl()
        {
            TestToControlPointData
                .ToList()
                .ForEach(v => AssertTestDataToControl(v));
        }

        private void AssertTestDataToMap((MapSettings MapSettings, SKPoint ControlPoint, SKPoint Answer) testData)
        {
            var mapPoint = CalculatePointHelper.ToMapPoint(testData.MapSettings, testData.ControlPoint);

            Assert.AreEqual(mapPoint, testData.Answer);
        }

        private void AssertTestDataToControl((MapSettings MapSettings, SKPoint ControlPoint, SKPoint Answer) testData)
        {
            var controlPoint = CalculatePointHelper.ToControlPoint(testData.MapSettings, testData.ControlPoint);

            Assert.AreEqual(controlPoint, testData.Answer);
        }
    }
}
