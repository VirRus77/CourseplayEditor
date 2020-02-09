namespace CourseEditor.Drawing.Controllers.Mouse
{
    /// <summary>
    /// 
    /// </summary>
    public interface IMouseOperationManager : IMouseEvents
    {
        /// <summary>
        /// Текущая операция мишки.
        /// </summary>
        IMouseOperation CurrentMouseOperation { get; }

        void StopOperation();
    }
}
