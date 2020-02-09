using System;
using CourseEditor.Drawing.LinearMath;
using CourseplayEditor.Contracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SkiaSharp;

namespace Courseplay.Tests
{
    [TestClass]
    public class MathTests
    {
        [TestMethod]
        public void PerpendicularTest()
        {
            var line = new SKLine(new SKPoint(-1, 0), new SKPoint(2, 0));
            var point = new SKPoint(0, 0);
            var rez = Perpendicular(line, point);
            var onLine = line.OnLine(rez);
            var dest = line.MinimalDistance(point);
        }

        [TestMethod]
        public void PointOnLine()
        {
            var line = new SKLine(new SKPoint(1, 0), new SKPoint(2, 0));
            var point = new SKPoint(0, 0);
            var rez = PointOnSegment(line, point);
        }

        private bool PointOnSegment(SKLine line, SKPoint point)
        {
            return LinearArithmetic.IsBetween(line.Point1.X, line.Point1.Y, line.Point2.X, line.Point2.Y, point.X, point.Y);
        }

        private bool PointOnLine(SKLine line, SKPoint point)
        {
            var x = line.Point1.X;
            var y = line.Point1.Y;
            var x0 = line.Point2.X;
            var y0 = line.Point2.Y;
            var dx = point.X;
            var dy = point.Y;

            var f0 = (y - y0);
            var f1 = (x - x0);
            return Math.Abs((dy - y0) * f1 - (dx - x0) * f0) < 0.0001;
        }

        private SKPoint Perpendicular(SKLine line, SKPoint point)
        {
            var a = line.Point1;
            var b = line.Point2;
            var c = point;
            var F0 = c.X - (b.Y - a.Y);
            var F1 = c.Y + (b.X - a.X);
            var k2 = ((c.X - a.X) * (b.Y - a.Y) - (b.X - a.X) * (c.Y - a.Y)) / ((b.X - a.X) * (F1 - c.Y) - (F0 - c.X) * (b.Y - a.Y));
            var d0 = (F0 - c.X) * k2 + c.X;
            var d1 = (F1 - c.Y) * k2 + c.Y;
            //perpendicular = New Point3d(d(0), d(1), 0)
            return new SKPoint(d0, d1);
        }
    }
}
