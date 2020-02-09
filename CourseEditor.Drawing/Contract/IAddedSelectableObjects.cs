namespace CourseEditor.Drawing.Contract
{
    /// <summary>
    /// Менеджер всех объектов которые могут быть выбранны
    /// </summary>
    public interface IAddedSelectableObjects<T> : ISelectableObjects
    {
        /// <summary>
        /// Добавить курсы в выделяемые объекты.
        /// </summary>
        /// <param name="selectableObjects">Выделяемые объекты.</param>
        void AddSelectableObjects(T selectableObjects);
    }
}
