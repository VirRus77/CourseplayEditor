using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace CourseEditor.Drawing.Contract
{
    /// <summary>
    /// Менеджер слоёв
    /// </summary>
    public interface IDrawLayerManager
    {
        /// <summary>
        /// Союытие при изменении <inheritdoc cref="Layers"/> или <inheritdoc cref="IDrawLayer"/>
        /// </summary>
        event EventHandler<EventArgs> Changed;

        /// <summary>
        /// Список слоев
        /// </summary>
        IReadOnlyCollection<IDrawLayer> Layers { get; }

        /// <summary>
        /// Добавить <inheritdoc cref="IDrawLayer"/>
        /// </summary>
        /// <param name="layer"><inheritdoc cref="IDrawLayer"/></param>
        void AddLayer([NotNull] in IDrawLayer layer);

        /// <summary>
        /// Вставить <inheritdoc cref="IDrawLayer"/> по позиции
        /// </summary>
        /// <param name="index">Индекс слоя в коллекции</param>
        /// <param name="layer"><inheritdoc cref="IDrawLayer"/></param>
        void InsertLayer(in int index, [NotNull] in IDrawLayer layer);

        /// <summary>
        /// Удалить <inheritdoc cref="IDrawLayer"/> по позиции
        /// </summary>
        /// <param name="index">Индекс слоя в коллекции</param>
        void RemoveLayer(in int index);
    }
}
