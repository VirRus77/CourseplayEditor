using System.Collections.Generic;
using System.Linq;
using Core.Tools.Extensions;
using CourseEditor.Drawing.Contract;
using CourseEditor.Drawing.Implementation;
using CourseEditor.Drawing.Tools;
using CourseplayEditor.Contracts;
using CourseplayEditor.Model;
using SkiaSharp;

namespace CourseplayEditor.Implementation.Layers
{
    public class OperationLayer : BaseDrawLayer, IOperationLayer
    {
        public const string SelectableItemsKey = "SelectableItems";

        private readonly ISelectableController _selectableController;
        private readonly IMapSettingsController _mapSettingsController;
        private readonly IManagedDrawSelectableObject _drawSelectableObject;

        private readonly IList<IOperationLayer.DrawAction> _drawActions;

        public OperationLayer(
            ISelectableController selectableController,
            IMapSettingsController mapSettingsController,
            IManagedDrawSelectableObject drawSelectableObject
        )
        {
            IsVisible = true;
            _selectableController = selectableController;
            _mapSettingsController = mapSettingsController;
            _drawSelectableObject = drawSelectableObject;
            _selectableController.Changed += SelectableControllerOnChanged;

            _drawActions = new List<IOperationLayer.DrawAction>();
        }

        public override void Draw(SKCanvas canvas, SKRect drawRect)
        {
            if (_selectableController.Value.Any())
            {
                DrawSelectedItems(canvas, drawRect, _selectableController.Value);
            }

            if (_drawActions.Any())
            {
                _drawActions.ForEach(v => v(canvas, drawRect));
            }
        }

        public void AddDraw(IOperationLayer.DrawAction drawAction)
        {
            _drawActions.Add(drawAction);
        }

        public void RemoveDraw(IOperationLayer.DrawAction drawAction)
        {
            _drawActions.Remove(drawAction);
        }

        private void DrawSelectedItems(
            SKCanvas canvas,
            SKRect drawRect,
            ICollection<ISelectable> values
        )
        {
            values
                .OfType<SplineMap>()
                .Where(v => v.Visible)
                .ForEach(v => _drawSelectableObject.Draw(SelectableItemsKey, canvas, drawRect, v));
            values
                .OfType<Course>()
                .Where(v => v.Visible)
                .ForEach(v => _drawSelectableObject.Draw(SelectableItemsKey, canvas, drawRect, v));
            values
                .OfType<Waypoint>()
                .Where(v => v.Course.Visible)
                .ForEach(v => _drawSelectableObject.Draw(SelectableItemsKey, canvas, drawRect, v));
        }

        private void SelectableControllerOnChanged(object? sender, ValueEventArgs<ICollection<ISelectable>> e)
        {
            RaiseChanged();
        }
    }
}
