using System.Collections.Generic;
using SkiaSharp;

namespace CourseEditor.Drawing.Contract
{
    public interface IChangeProvider
    {
        void Move(ICollection<ISelectableObjects> objects, SKPoint delta);

        void Add(ICollection<ISelectableObjects> objects);

        void Delete(ICollection<ISelectableObjects> objects);
    }
}
