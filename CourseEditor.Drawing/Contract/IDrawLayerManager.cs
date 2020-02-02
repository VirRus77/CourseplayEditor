using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

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
        /// Список слоёв
        /// </summary>
        IReadOnlyCollection<IDrawLayer> Layers { get; }

        /// <summary>
        /// Добавить <inheritdoc cref="IDrawLayer"/>
        /// </summary>
        /// <param name="layer"><inheritdoc cref="IDrawLayer"/></param>
        void AddLayer([NotNull] in IDrawLayer layer);

        /// <summary>
        /// Добавить коллекцию <inheritdoc cref="IDrawLayer"/>
        /// </summary>
        /// <param name="layers">Перечисление <inheritdoc cref="IDrawLayer"/></param>
        void AddLayers([NotNull] in IEnumerable<IDrawLayer> layers);

        /// <summary>
        /// Вставить <inheritdoc cref="IDrawLayer"/> по позиции
        /// </summary>
        /// <param name="index">Индекс слоя в коллекции</param>
        /// <param name="layer"><inheritdoc cref="IDrawLayer"/></param>
        void InsertLayer(in int index, [NotNull] in IDrawLayer layer);

        /// <summary>
        /// Вставить коллекцию <inheritdoc cref="IDrawLayer"/> по позиции
        /// </summary>
        /// <param name="index">Индекс слоя в коллекции</param>
        /// <param name="layers">Перечисление <inheritdoc cref="IDrawLayer"/></param>
        void InsertLayers(in int index, [NotNull] in IEnumerable<IDrawLayer> layers);

        /// <summary>
        /// Удалить <inheritdoc cref="IDrawLayer"/> по позиции
        /// </summary>
        /// <param name="index">Индекс слоя в коллекции</param>
        void RemoveLayer(in int index);

        /// <summary>
        /// Удалить <inheritdoc cref="IDrawLayer"/> по значению
        /// </summary>
        /// <param name="layer"><inheritdoc cref="IDrawLayer"/></param>
        void RemoveLayer([NotNull] in IDrawLayer layer);

        /// <summary>
        /// Удалить коллекцию <inheritdoc cref="IDrawLayer"/> по значениям
        /// </summary>
        /// <param name="layers">Перечисление <inheritdoc cref="IDrawLayer"/></param>
        void RemoveLayers([NotNull] in IEnumerable<IDrawLayer> layers);

        /// <summary>
        /// Получить индекс <inheritdoc cref="IDrawLayer"/>
        /// </summary>
        /// <param name="layer"><inheritdoc cref="IDrawLayer"/></param>
        int IndexOf([NotNull] in IDrawLayer layer);
    }
}
