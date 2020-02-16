using System.Collections.Generic;
using CourseEditor.Drawing.Contract;
using CourseplayEditor.Contracts;
using SkiaSharp;

namespace CourseplayEditor.Implementation
{
    public abstract class HashedSelectableObject<TValue> : IHashedSelectableObject<TValue>
        where TValue : ISelectable
    {

        protected HashedSelectableObject(TValue value)
        {
            Value = value;
        }

        public TValue Value { get; }

        public abstract ICollection<ISelectable> Intersect(SKPoint point, float radius);
        public abstract ICollection<ISelectable> Intersect(SKRect rect);

        protected static bool InRect(SKLine skLine, SKRect rect)
        {
            return InRect(skLine.Point1, rect) && InRect(skLine.Point2, rect);
        }

        protected static bool InRect(SKPoint skLinePoint1, SKRect rect)
        {
            return skLinePoint1.X >= rect.Left &&
                   skLinePoint1.X <= rect.Right &&
                   skLinePoint1.Y >= rect.Top &&
                   skLinePoint1.Y <= rect.Bottom;
        }
    }
}
