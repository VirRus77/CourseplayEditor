namespace CourseEditor.Drawing.Contract
{
    /// <summary>
    /// <para lang="ru">Менеджер всех объектов которые могут быть выбранны</para>
    /// </summary>
    public interface IAddedSelectableObjects<T> : ISelectableObjects
    {
        /// <summary>
        /// <para lang="ru">Добавить курсы в выделяемые объекты.</para>
        /// </summary>
        /// <param name="selectableObjects">
        /// <para lang="ru">Выделяемые объекты.</para>
        /// </param>
        void AddSelectableObjects(T selectableObjects);
    }
}
