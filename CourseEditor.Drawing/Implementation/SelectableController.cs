using System.Collections.Generic;
using CourseEditor.Drawing.Contract;

namespace CourseEditor.Drawing.Implementation
{
    /// <inheritdoc />
    public class SelectableController : ValueController<ICollection<ISelectable>>, ISelectableController
    {
        public SelectableController()
            : base(new List<ISelectable>())
        {
        }

        public void Select(ISelectable value)
        {
            var changing = BeginChanging();

            Value.Clear();
            Value.Add(value);

            changing.Dispose();
        }
    }
}
