using System.Collections.Generic;

namespace CourseEditor.Drawing.Contract
{
    public interface ISelectableController: IValueController<ICollection<ISelectable>>
    {
        void Select(ISelectable value);

        void Select(IEnumerable<ISelectable> value);

        void ClearSelect();
    }
}
