using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using CourseEditor.Drawing.Contract;

namespace CourseEditor.Drawing.Implementation
{
    /// <inheritdoc cref="IDrawLayerManager"/>
    public class DrawLayerManager : IDrawLayerManager
    {
        private readonly List<IDrawLayer> _layers;

        /// <summary>
        /// Конструктор <inheritdoc cref="DrawLayerManager"/>
        /// </summary>
        public DrawLayerManager()
        {
            _layers = new List<IDrawLayer>();
        }

        /// <inheritdoc />
        public event EventHandler<EventArgs> Changed;

        /// <inheritdoc />
        public IReadOnlyCollection<IDrawLayer> Layers => _layers;

        /// <inheritdoc />
        public void AddLayer(in IDrawLayer layer)
        {
            if (layer == null)
            {
                throw new ArgumentNullException(nameof(layer));
            }

            _layers.Add(layer);
            layer.Changed += LayerOnChanged;
            RaiseChanged();
        }

        /// <inheritdoc />
        public void InsertLayer(in int index, [NotNull]in IDrawLayer layer)
        {
            if (index < 0 || index >= _layers.Count)
            {
                throw new ArgumentOutOfRangeException(nameof(index));
            }
            if (layer == null)
            {
                throw new ArgumentNullException(nameof(layer));
            }

            _layers.Insert(index, layer);
            RaiseChanged();
        }

        /// <inheritdoc />
        public void RemoveLayer(in int index)
        {
            if (index < 0 || index >= _layers.Count)
            {
                throw new ArgumentOutOfRangeException(nameof(index));
            }

            var layer = _layers[index];
            layer.Changed -= LayerOnChanged;
            _layers.RemoveAt(index);
            RaiseChanged();
        }

        /// <summary>
        /// Вызвать <see cref="Changed"/>
        /// </summary>
        protected void RaiseChanged()
        {
            Changed?.Invoke(this, EventArgs.Empty);
        }

        private void LayerOnChanged(object? sender, EventArgs e)
        {
            RaiseChanged();
        }
    }
}
