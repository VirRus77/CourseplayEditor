using System;
using System.Collections.Generic;
using CourseEditor.Drawing.Contract;
using SkiaSharp;

namespace CourseEditor.Drawing.Controllers.Implementation
{
    /// <inheritdoc />
    public class ChangeProvider : IChangeProvider
    {
        public void Move(ICollection<ISelectableObjects> objects, SKPoint delta)
        {
            throw new NotImplementedException();
        }

        public void Add(ICollection<ISelectableObjects> objects)
        {
            throw new NotImplementedException();
        }

        public void Delete(ICollection<ISelectableObjects> objects)
        {
            throw new NotImplementedException();
        }
    }
}
