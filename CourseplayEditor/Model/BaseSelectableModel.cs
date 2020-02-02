using CourseEditor.Drawing.Contract;

namespace CourseplayEditor.Model
{
    public class BaseSelectableModel : BaseModel, ISelectable
    {
        private bool _selected;

        /// <inheritdoc />
        public bool Selected
        {
            get => _selected;
            set => SetValue(ref _selected, value);
        }
    }
}
