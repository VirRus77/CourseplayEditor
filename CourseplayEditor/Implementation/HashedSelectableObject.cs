using System.Collections.Generic;
using CourseEditor.Drawing.Contract;
using SkiaSharp;

namespace CourseplayEditor.Implementation
{
    public abstract class HashedSelectableObject<TValue> : IHashedSelectableObject
        where TValue : ISelectable
    {

        protected HashedSelectableObject(TValue value)
        {
            Value = value;
        }

        internal TValue Value { get; }

        public abstract ICollection<ISelectable> Intersect(SKPoint point, float radius);
    }
}
