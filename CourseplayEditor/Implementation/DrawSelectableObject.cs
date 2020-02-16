using System;
using System.Collections.Generic;
using System.Linq;
using CourseEditor.Drawing.Contract;
using CourseEditor.Drawing.Contract.Configuration;
using CourseEditor.Drawing.Controllers.Implementation;
using CourseplayEditor.Contracts;
using CourseplayEditor.Model;
using MoreLinq.Extensions;
using SkiaSharp;

namespace CourseplayEditor.Implementation
{
    public class DrawSelectableObject : IManagedDrawSelectableObject
    {
        private readonly IDrawConfiguration _drawConfiguration;
        private readonly IMapSettingsController _mapSettingsController;

        private MapSettings MapSettings => _mapSettingsController.Value;

        public DrawSelectableObject(
            IDrawConfiguration drawConfiguration,
            IMapSettingsController mapSettingsController
        )
        {
            _drawConfiguration = drawConfiguration;
            _mapSettingsController = mapSettingsController;
        }

        public void DrawCircle(string drawLayerKey, SKCanvas canvas, SKRect drawRect, SKPoint centerPoint, float radius)
        {
            var paint = _drawConfiguration[drawLayerKey];
            DrawCircle(paint, canvas, drawRect, centerPoint, radius);
        }

        public void DrawLines(string drawLayerKey, SKCanvas canvas, SKRect drawRect, ICollection<SKPoint> points)
        {
            var paint = _drawConfiguration[drawLayerKey];
            DrawLines(paint, canvas, drawRect, points);
        }

        public void DrawGradientLines(string drawLayerKey, SKCanvas canvas, SKRect drawRect, ICollection<SKPoint> points)
        {
            var paints = _drawConfiguration.Gradient(drawLayerKey);
            DrawGrdientLines(paints, canvas, drawRect, points);
        }

        public void Draw(string selectableItemsKey, SKCanvas canvas, SKRect drawRect, SplineMap splineMap)
        {
            var paint = _drawConfiguration[selectableItemsKey];
            DrawLines(paint, canvas, drawRect, splineMap.Points.Select(v => ToPoint(v)).ToArray());
            splineMap.Points
                .Select(v => ToPoint(v))
                .ForEach(point => DrawPoint(paint, canvas, drawRect, point));
        }

        public void Draw(string selectableItemsKey, SKCanvas canvas, SKRect drawRect, Course course)
        {
            if (_drawConfiguration.IsGradient(selectableItemsKey))
            {
                var paints = _drawConfiguration.Gradient(selectableItemsKey);
                DrawGrdientLines(paints, canvas, drawRect, course.Waypoints.Select(v => ToPoint(v)).ToArray());
                course.Waypoints
                    .Select(v => ToPoint(v))
                    .ForEach(point => DrawPoint(paints.First(), canvas, drawRect, point));
            }
            else
            {
                var paint = _drawConfiguration[selectableItemsKey];
                DrawLines(paint, canvas, drawRect, course.Waypoints.Select(v => ToPoint(v)).ToArray());
                course.Waypoints
                    .Select(v => ToPoint(v))
                    .ForEach(point => DrawPoint(paint, canvas, drawRect, point));
            }
        }

        public void Draw(string selectableItemsKey, SKCanvas canvas, SKRect drawRect, Waypoint waypoint)
        {
            var paint = _drawConfiguration[selectableItemsKey];
            var point = ToPoint(waypoint);
            DrawPoint(paint, canvas, drawRect, point);
        }

        private void DrawGrdientLines(ICollection<SKPaint> paints, SKCanvas canvas, SKRect drawRect, ICollection<SKPoint> points)
        {
            if (paints.Count < 2)
            {
                throw new Exception();
            }

            try
            {
                var start = points.First();
                var distance = points
                    .Skip(1)
                    .Select(
                        nextPoint =>
                        {
                            var distance = SKPoint.Distance(start, nextPoint);
                            start = nextPoint;
                            return distance;
                        }
                    )
                    .ToArray();

                var fullDistance = distance.Sum();

                var dist = 0f;
                start = points.First();
                points.Skip(1)
                    .ForEach(
                        (nextPoint, index) =>
                        {
                            using var paint = new SKPaint
                            {
                                Shader = SKShader.CreateLinearGradient(
                                    start,
                                    nextPoint,
                                    new[]
                                    {
                                        CalculateColor(dist / fullDistance, paints.Select(v => v.Color).ToArray()),
                                        CalculateColor((dist + distance[index]) / fullDistance, paints.Select(v => v.Color).ToArray()),
                                    },
                                    SKShaderTileMode.Clamp
                                )
                            };
                            dist += distance[index];
                            canvas.DrawLine(start, nextPoint, paint);
                            start = nextPoint;
                        }
                    );
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        private SKColor CalculateColor(float distance, ICollection<SKColor> colors)
        {
            try
            {
                var countDistance = colors.Count - 1;
                var distanceDelta = 1f / countDistance;
                var indexDistance = (int)(distance / distanceDelta);
                indexDistance = Math.Min(indexDistance, colors.Count - 2);
                var percentDistance = (distance - (indexDistance * distanceDelta)) / distanceDelta;
                return CalculateColor(
                    percentDistance,
                    colors.ElementAt(indexDistance),
                    colors.ElementAt(indexDistance + 1)
                );
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        private SKColor CalculateColor(in float dist, SKColor color1, SKColor color2)
        {
            if (dist < 0)
            {
                throw new Exception();
            }

            var deltaRed = (color2.Red - color1.Red) * dist;
            var deltaGreen = (color2.Green - color1.Green) * dist;
            var deltaBlue = (color2.Blue - color1.Blue) * dist;
            var deltaAlpha = (color2.Alpha - color1.Alpha) * dist;
            var red = color1.Red + deltaRed;
            var green = color1.Green + deltaGreen;
            var blue = color1.Blue + deltaBlue;
            var alpha = color1.Alpha + deltaAlpha;
            //red = float.IsNormal(red)
            //    ? red
            //    : color1.Red;
            //green = float.IsNormal(green)
            //    ? green
            //    : color1.Green;
            //blue = float.IsNormal(blue)
            //    ? blue
            //    : color1.Blue;
            //alpha = float.IsNormal(alpha)
            //    ? alpha
            //    : color1.Alpha;
            return new SKColor((byte)red, (byte)green, (byte)blue, (byte)alpha);
        }

        private void DrawCircle(SKPaint paint, SKCanvas canvas, SKRect drawRect, SKPoint centerPoint, in float radius)
        {
            canvas.DrawCircle(centerPoint, radius, paint);
        }

        private void DrawPoint(SKPaint paint, SKCanvas canvas, SKRect drawRect, SKPoint point)
        {
            var rectSize = 5f / _mapSettingsController.Value.Scale;
            canvas.DrawRect(point.X - rectSize / 2f, point.Y - rectSize / 2f, rectSize, rectSize, paint);
        }

        private void DrawLines(SKPaint paint, SKCanvas canvas, SKRect drawRect, ICollection<SKPoint> points)
        {
            var start = points.First();
            points.Skip(1)
                .ForEach(
                    v =>
                    {
                        canvas.DrawLine(start, v, paint);
                        start = v;
                    }
                );
        }

        private static SKPoint ToPoint(Waypoint point)
        {
            return ToPoint(point.Point);
        }

        private static SKPoint ToPoint(SKPoint3 point)
        {
            return new SKPoint(point.X, point.Y);
        }
    }
}
