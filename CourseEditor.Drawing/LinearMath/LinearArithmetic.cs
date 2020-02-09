using System;

namespace CourseEditor.Drawing.LinearMath
{
    public static class LinearArithmetic
    {
        private const float Tolerance = 0.0001f;

        /// <summary>
        /// https://adn-cis.org/forum/index.php?PHPSESSID=s361ihh6car9fkhprqt903hu80&topic=3328.msg14181#msg14181
        /// </summary>
        /// <param name="aX"></param>
        /// <param name="aY"></param>
        /// <param name="bX"></param>
        /// <param name="bY"></param>
        /// <param name="cX"></param>
        /// <param name="cY"></param>
        /// <param name="pX"></param>
        /// <param name="pY"></param>
        public static void Perpendicular(
            in float aX,
            in float aY,
            in float bX,
            in float bY,
            in float cX,
            in float cY,
            out float pX,
            out float pY
        )
        {
            var f0 = cX - (bY - aY);
            var f1 = cY + (bX - aX);
            var k2 = ((cX - aX) * (bY - aY) - (bX - aX) * (cY - aY)) / ((bX - aX) * (f1 - cY) - (f0 - cX) * (bY - aY));
            pX = (f0 - cX) * k2 + cX;
            pY = (f1 - cY) * k2 + cY;
        }

        public static float Distance(in float aX, in float aY, in float bX, in float bY)
        {
            var f0 = (aX - bX);
            var f1 = (aY - bY);
            return (float) Math.Sqrt(f0 * f0 + f1 * f1);
        }

        /// <summary>
        /// https://stackoverflow.com/a/328193/6098146
        /// </summary>
        /// <param name="aX"></param>
        /// <param name="aY"></param>
        /// <param name="bX"></param>
        /// <param name="bY"></param>
        /// <param name="cX"></param>
        /// <param name="cY"></param>
        /// <param name="tolerance"></param>
        /// <returns></returns>
        public static bool IsBetween(
            in float aX,
            in float aY,
            in float bX,
            in float bY,
            in float cX,
            in float cY,
            float tolerance = Tolerance
        )
        {
            return Math.Abs(Distance(aX, aY, cX, cY) + Distance(bX, bY, cX, cY) - Distance(aX, aY, bX, bY)) < tolerance;
        }
    }
}
