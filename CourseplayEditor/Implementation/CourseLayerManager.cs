using System.Collections.Generic;
using System.Linq;
using CourseEditor.Drawing.Contract;
using CourseplayEditor.Contracts;
using CourseplayEditor.Tools.Courseplay.v2019;
using I3DShapesTool.Lib.Model;

namespace CourseplayEditor.Implementation
{
    /// <inheritdoc />
    public class CourseLayerManager : ICourseLayerManager
    {
        private readonly IDrawLayerManager _drawLayerManager;
        private readonly ICollection<IDrawLayer> _courseLayers;
        private readonly ICollection<IDrawLayer> _mapSplines;
        private BackgroundMapDrawLayer _mapBackgroundLayer;

        /// <summary>
        /// Конструктор <inheritdoc cref="CourseLayerManager"/>
        /// </summary>
        /// <param name="drawLayerManager"></param>
        public CourseLayerManager(IDrawLayerManager drawLayerManager)
        {
            _drawLayerManager = drawLayerManager;
            Initialize(_drawLayerManager);
            _courseLayers = new List<IDrawLayer>();
            _mapSplines = new List<IDrawLayer>();
        }

        private void Initialize(IDrawLayerManager drawLayerManager)
        {
            _mapBackgroundLayer = new BackgroundMapDrawLayer();
            if (drawLayerManager.Layers.Count == 0)
            {
                drawLayerManager.AddLayer(_mapBackgroundLayer);
            }
            else
            {
                drawLayerManager.InsertLayer(0, _mapBackgroundLayer);
            }
        }

        public void AddCourses(in ICollection<Course> courses)
        {
            _drawLayerManager.RemoveLayers(_courseLayers);
            _drawLayerManager.AddLayers(
                courses.Select(
                    course =>
                    {
                        var layer = new CourseDrawLayer();
                        layer.Load(course);
                        return layer;
                    }
                )
            );
        }

        public void AddBackgroundMap(in string fileName)
        {
            _mapBackgroundLayer.OpenImage(fileName);
        }

        public void AddMapSplines(in IEnumerable<Spline> splines)
        {
            var index = _drawLayerManager.IndexOf(_mapBackgroundLayer);
            var layers = splines.Select(
                spline =>
                {
                    var layer = new SplineDrawLayer();
                    layer.Load(spline);
                    return layer;
                }
            );
            if (_drawLayerManager.Layers.Count == index + 1)
            {
                _drawLayerManager.AddLayers(layers);
            }
            else
            {
                _drawLayerManager.InsertLayers(index + 1, layers);
            }
        }
    }
}
