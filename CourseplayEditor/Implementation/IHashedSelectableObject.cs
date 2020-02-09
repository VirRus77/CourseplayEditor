using System.Collections.Generic;
using CourseEditor.Drawing.Contract;
using SkiaSharp;

namespace CourseplayEditor.Implementation
{
    interface IHashedSelectableObject
    {
        ICollection<ISelectable> Intersect(SKPoint point, float radius);
    }
}
