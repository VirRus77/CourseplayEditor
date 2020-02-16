using System.Collections.Generic;
using System.Linq;
using Core.Tools.Extensions;
using CourseEditor.Drawing.Contract.Configuration;
using SkiaSharp;

namespace CourseEditor.Drawing.Implementation.Configuration
{
    public class DrawConfiguration : IDrawConfiguration
    {
        private readonly IDictionary<string, SKPaint> _drawConfigurations;
        private readonly ICollection<string> _isGradient;

        public DrawConfiguration()
        {
            _drawConfigurations = new Dictionary<string, SKPaint>();
            _isGradient = new List<string>();
        }

        public SKPaint this[string key] => _drawConfigurations[key];

        public ICollection<SKPaint> Gradient(string key)
        {
            return GetGradient(key).ToArray();
        }

        public bool IsGradient(string key)
        {
            return _isGradient.Contains(key);
        }

        public void Add(string key, SKPaint paint)
        {
            _drawConfigurations.Add(key, paint);
        }

        public void AddGradient(string key, params SKPaint[] paints)
        {
            paints
                .ForEach((v, index) => Add(GradientName(key, index), v));
            _isGradient.Add(key);
        }
        private IEnumerable<SKPaint> GetGradient(string key)
        {
            var index = 0;
            while (true)
            {
                var gradientName = GradientName(key, index++);
                if (!_drawConfigurations.TryGetValue(gradientName, out var paint))
                {
                    break;
                }
                yield return paint;
            }
        }
        private static string GradientName(string key, int index)
        {
            return $"{key}_Gradient{index + 1}";
        }
    }
}
