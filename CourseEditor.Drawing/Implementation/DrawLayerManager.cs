using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Core.Tools.Extensions;
using CourseEditor.Drawing.Contract;
using CourseEditor.Drawing.Tools;

namespace CourseEditor.Drawing.Implementation
{
    /// <inheritdoc cref="IDrawLayerManager"/>
    public class DrawLayerManager : IDrawLayerManager
    {
        private bool _changing = false;
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
        public void AddLayer([NotNull] in IDrawLayer layer)
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
        public void AddLayers([NotNull] in IEnumerable<IDrawLayer> layers)
        {
            if (!layers.Any())
            {
                return;
            }

            layers.ForEach(
                layer =>
                {
                    layer.Changed += LayerOnChanged;
                    _layers.Add(layer);
                }
            );
            RaiseChanged();
        }

        /// <inheritdoc />
        public void InsertLayer(in int index, [NotNull] in IDrawLayer layer)
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
        public void InsertLayers(in int index, [NotNull] in IEnumerable<IDrawLayer> layers)
        {
            if (index < 0 || index >= _layers.Count)
            {
                throw new ArgumentOutOfRangeException(nameof(index));
            }

            if (layers == null)
            {
                throw new ArgumentNullException(nameof(layers));
            }

            var localIndex = index;
            layers.ForEach((layer, i) => _layers.Insert(localIndex + i, layer));
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

        /// <inheritdoc />
        public void RemoveLayer([NotNull] in IDrawLayer layer)
        {
            layer.Changed -= LayerOnChanged;
            _layers.Remove(layer);
            RaiseChanged();
        }

        /// <inheritdoc />
        public void RemoveLayers([NotNull] in IEnumerable<IDrawLayer> layers)
        {
            if (!layers.Any())
            {
                return;
            }

            layers.ForEach(
                layer =>
                {
                    layer.Changed -= LayerOnChanged;
                    _layers.Remove(layer);
                }
            );
            RaiseChanged();
        }

        /// <inheritdoc />
        public int IndexOf([NotNull] in IDrawLayer layer)
        {
            return _layers.IndexOf(layer);
        }

        /// <inheritdoc />
        public IDisposable BeginChanging()
        {
            if (_changing)
            {
                return DisposeAction.Empty();
            }

            _changing = true;
            
            return new DisposeAction(
                () =>
                {
                    _changing = false;
                    RaiseChanged();
                }
            );
        }

        /// <summary>
        /// Вызвать <see cref="Changed"/>
        /// </summary>
        protected void RaiseChanged()
        {
            if (_changing)
            {
                return;
            }

            Changed?.Invoke(this, EventArgs.Empty);
        }

        private void LayerOnChanged(object? sender, EventArgs e)
        {
            RaiseChanged();
        }
    }
}
