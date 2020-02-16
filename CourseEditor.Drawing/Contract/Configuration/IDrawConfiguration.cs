using System.Collections.Generic;
using SkiaSharp;

namespace CourseEditor.Drawing.Contract.Configuration
{
    public interface IDrawConfiguration
    {
        SKPaint this[string key] { get; }

        ICollection<SKPaint> Gradient(string key);

        bool IsGradient(string key);

        void Add(string key, SKPaint paint);

        void AddGradient(string key, params SKPaint[] paints);
    }
}
