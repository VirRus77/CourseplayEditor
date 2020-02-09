using CourseEditor.Drawing.LinearMath;
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
            //            /*
            //A(xa,ya), B(xb,yb), C(xc,yc).
            //AB(dx,dy) = (xb-xa, yb-ya).
            //BC(dy,-dx) //поверь на слово
            //C = B + BC = (xb+dy, yb-dx) = (xb+yb-ya, yb+xa-xb).
            //             */
            //            var xa = Point1.X;
            //            var ya = Point1.Y;
            //            var xb = Point2.X;
            //            var yb = Point2.Y;
            //            var xc = point.X;
            //            var yc = point.Y;

            //            var x = ((xa - xb) * xc / (yb - ya) - (yb - ya) * xa / (xb - xa) + ya - yc) /
            //                    ((xa - xb) / (yb - ya) - (yb - ya) / (xb - xa));
            //            var y = (xa - xb) * x / (yb - ya) - (xa - xb) * xc / (yb - ya) + yc;

            LinearArithmetic.Perpendicular(Point1.X, Point1.Y, Point2.X, Point2.Y, point.X, point.Y, out var pointX, out var pointY);
            return new SKPoint(pointX, pointY);
        }

        public bool OnLine(SKPoint point)
        {
            return LinearArithmetic.IsBetween(Point1.X, Point1.Y, Point2.X, Point2.Y, point.X, point.Y);
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
