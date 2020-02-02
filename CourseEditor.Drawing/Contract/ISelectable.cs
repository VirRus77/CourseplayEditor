namespace CourseEditor.Drawing.Contract
{
    /// <summary>
    /// An element that can be selected.
    /// </summary>
    public interface ISelectable
    {
        /// <summary>
        /// Selected flag.
        /// </summary>
        bool Selected { get; set; }
    }
}
