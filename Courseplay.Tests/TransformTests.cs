using System.Collections.Generic;
using System.Linq;
using CourseEditor.Drawing.Controllers.Implementation;
using CourseEditor.Drawing.Tools;
using NUnit.Framework;
using SkiaSharp;

namespace Courseplay.Tests
{
    public class TransformTests
    {
        private readonly static ICollection<(MapSettings MapSettings, SKPoint ControlPoint, SKPoint Answer)> TestToMapPointData = new[]
        {
            (new MapSettings(new SKPoint(0,0),1), new SKPoint(50, 100), new SKPoint(50,100)),
            (new MapSettings(new SKPoint(50,25),1), new SKPoint(50, 100), new SKPoint(100,125)),
            (new MapSettings(new SKPoint(-50,-25),1), new SKPoint(50, 100), new SKPoint(0,75)),
            (new MapSettings(new SKPoint(-50,-25),1), new SKPoint(10, 20), new SKPoint(-40,-5)),
            (new MapSettings(new SKPoint(-50,-25),2), new SKPoint(50, 100), new SKPoint(50,175)),
        };

        private readonly static ICollection<(MapSettings MapSettings, SKPoint ControlPoint, SKPoint Answer)> TestToControlPointData = new[]
        {
            (new MapSettings(new SKPoint(0,0),1), new SKPoint(50, 100), new SKPoint(50,100)),
            (new MapSettings(new SKPoint(50,25),1), new SKPoint(50, 100), new SKPoint(0,75)),
            (new MapSettings(new SKPoint(-50,-25),1), new SKPoint(50, 100), new SKPoint(100,125)),
            (new MapSettings(new SKPoint(-50,-25),1), new SKPoint(10, 20), new SKPoint(60,45)),
            (new MapSettings(new SKPoint(-50,-25),2f), new SKPoint(50, 100), new SKPoint(100/2f,125/2f)),
        };

        [Test]
        public void TestToMap()
        {
            TestToMapPointData
                .ToList()
                .ForEach(v => AssertTestDataToMap(v));
        }

        [Test]
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
        //[Test]
        //public void TestToMap()
        //{
        //    var mapSettings = new MapSettings(new SKPoint(0,0),1);
        //    var controlPoint = new SKPoint(50,100);
        //    var mapPoint = CalculatePointHelper.ToMapPoint(mapSettings, controlPoint);

        //    Assert.AreEqual(mapPoint, new SKPoint(50, 100));
        //}

        //[Test]
        //public void TestToMap2()
        //{
        //    var mapSettings = new MapSettings(new SKPoint(-50, -25), 1);
        //    var controlPoint = new SKPoint(50, 100);
        //    var mapPoint = CalculatePointHelper.ToMapPoint(mapSettings, controlPoint);

        //    Assert.AreEqual(mapPoint, new SKPoint(100, 125));
        //}
    }
}
