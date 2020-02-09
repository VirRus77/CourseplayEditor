using System;
using SkiaSharp;

namespace CourseplayEditor.Contracts
{
    public class SKLine
    {
        public SKLine(SKPoint point1, SKPoint point2)
        {
            Point1 = point1;
            Point2 = point2;
        }

        public SKPoint Point1 { get; }

        public SKPoint Point2 { get; }

        public SKPoint PerpendicularPointOnLine(SKPoint point)
        {
            /*
A(xa,ya), B(xb,yb), C(xc,yc).
AB(dx,dy) = (xb-xa, yb-ya).
BC(dy,-dx) //поверь на слово
C = B + BC = (xb+dy, yb-dx) = (xb+yb-ya, yb+xa-xb).
             */
            var xa = Point1.X;
            var ya = Point1.Y;
            var xb = Point2.X;
            var yb = Point2.Y;
            var xc = point.X;
            var yc = point.Y;
            return new SKPoint(xb + yb - ya, yb + xa - xb);
        }

        public bool OnLine(SKPoint point)
        {
            /*
A(x1, y1) и B(x2, y2), C(x, y)
dx1 = x2 - x1;
dy1 = y2 - y1;

dx = x - x1;
dy = y - y1;

S = dx1 * dy - dx * dy1;
             */
            var dx1 = Point2.X - Point1.X;
            var dy1 = Point2.Y - Point1.Y;
            var dx = point.X - Point1.X;
            var dy = point.Y - Point1.Y;
            return Math.Abs(dx1 * dy - dx * dy1) < 0.00001;
        }

        public float MinimalDistance(SKPoint point)
        {
            var pointOnLine = PerpendicularPointOnLine(point);
            var destPerpendicular = OnLine(pointOnLine) ? SKPoint.Distance(pointOnLine, point) : default(float?);
            var dest1 = SKPoint.Distance(point, Point1);
            var dest2 = SKPoint.Distance(point, Point2);
            var min = dest1;
            if (dest2 < min)
            {
                min = dest2;
            }

            if (destPerpendicular.HasValue && destPerpendicular.Value < min)
            {
                min = destPerpendicular.Value;
            }

            return min;
        }
    }
}
