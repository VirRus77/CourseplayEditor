namespace CourseEditor.Drawing.Controllers.Mouse
{
    public interface IMouseOperation : IMouseEvents
    {
        /// <summary>
        /// Запущена ли операция.
        /// </summary>
        bool IsRun { get; }

        /// <summary>
        /// Тип операции.
        /// </summary>
        MouseOperationType OperationType { get; }

        /// <summary>
        /// Поддерживаемые события мышки.
        /// </summary>
        MouseEventType SupportMouseEvent { get; }

        /// <summary>
        /// Остановить операцию
        /// </summary>
        void Stop();
    }
}
