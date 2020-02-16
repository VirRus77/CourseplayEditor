using System;
using System.Collections.Generic;
using System.Linq;
using Core.Tools.Extensions;
using CourseEditor.Drawing.Contract;
using CourseplayEditor.Contracts;
using CourseplayEditor.Model;
using CourseplayEditor.Tools.Extensions;

namespace CourseplayEditor.Implementation.Layers
{
    /// <inheritdoc />
    public class CourseLayerManager : ICourseLayerManager
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IDrawLayerManager _drawLayerManager;
        private readonly ICollection<IDrawLayer> _courseLayers;
        private readonly IOperationLayer _operationLayer;
        private readonly ICollection<IDrawLayer> _mapSplines;
        private BackgroundMapDrawLayer _mapBackgroundLayer;

        /// <summary>
        /// Конструктор <inheritdoc cref="CourseLayerManager"/>
        /// </summary>
        /// <param name="drawLayerManager"></param>
        public CourseLayerManager(
            IServiceProvider serviceProvider,
            IDrawLayerManager drawLayerManager,
            IOperationLayer operationLayer
        )
        {
            _serviceProvider = serviceProvider;
            _drawLayerManager = drawLayerManager;
            _operationLayer = operationLayer;
            Initialize(_drawLayerManager);
            _courseLayers = new List<IDrawLayer>();
            _mapSplines = new List<IDrawLayer>();
        }

        private void Initialize(IDrawLayerManager drawLayerManager)
        {
            var changed = drawLayerManager.BeginChanging();

            _mapBackgroundLayer = new BackgroundMapDrawLayer();
            if (drawLayerManager.Layers.Count == 0)
            {
                drawLayerManager.AddLayer(_mapBackgroundLayer);
            }
            else
            {
                drawLayerManager.InsertLayer(0, _mapBackgroundLayer);
            }

            drawLayerManager.AddLayer(_operationLayer);

            ReindexSystemLayers();

            changed.Dispose();
        }

        public void AddCourses(in ICollection<Course> courses)
        {
            var changed = _drawLayerManager.BeginChanging();

            _drawLayerManager.RemoveLayers(_courseLayers);
            _courseLayers.Clear();
            courses
                .Select(
                    course =>
                    {
                        var layer = _serviceProvider.CreateInstance<CourseDrawLayer>();
                        layer.Load(course);
                        return layer;
                    }
                )
                .ForEach(courseLayer => _courseLayers.Add(courseLayer));

            _drawLayerManager.AddLayers(_courseLayers);

            ReindexSystemLayers();

            changed.Dispose();
        }

        public void AddBackgroundMap(in string fileName)
        {
            _mapBackgroundLayer.OpenImage(fileName);
        }

        public void AddMapSplines(in IEnumerable<SplineMap> splines)
        {
            var changed = _drawLayerManager.BeginChanging();

            var index = _drawLayerManager.IndexOf(_mapBackgroundLayer);

            _drawLayerManager.RemoveLayers(_mapSplines);
            _mapSplines.Clear();
            splines
                .Select(
                    spline =>
                    {
                        var layer = _serviceProvider.CreateInstance<SplineDrawLayer>();
                        layer.Load(spline);
                        return layer;
                    }
                )
                .ForEach(v => _mapSplines.Add(v));

            if (_drawLayerManager.Layers.Count == index + 1)
            {
                _drawLayerManager.AddLayers(_mapSplines);
            }
            else
            {
                _drawLayerManager.InsertLayers(index + 1, _mapSplines);
            }

            ReindexSystemLayers();

            changed.Dispose();
        }

        private void ReindexSystemLayers()
        {
            var currentIndex = _drawLayerManager.IndexOf(_operationLayer);
            if (currentIndex == _drawLayerManager.Layers.Count - 1)
            {
                return;
            }

            var changing = _drawLayerManager.BeginChanging();

            _drawLayerManager.RemoveLayer(_operationLayer);
            _drawLayerManager.AddLayer(_operationLayer);

            changing.Dispose();
        }
    }
}
