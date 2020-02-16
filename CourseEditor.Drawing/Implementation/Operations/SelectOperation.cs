using System;
using System.Linq;
using System.Windows.Input;
using Core.Tools.Extensions;
using CourseEditor.Drawing.Contract;
using CourseEditor.Drawing.Contract.Operations;
using CourseEditor.Drawing.Controllers.Implementation;
using CourseEditor.Drawing.Implementation.Configuration;
using CourseEditor.Drawing.Tools;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SkiaSharp;
using CursorType = CourseEditor.Drawing.Contract.CursorType;

namespace CourseEditor.Drawing.Implementation.Operations
{
    public class SelectOperation : BaseOperation, ISelectOperation
    {
        private readonly IManagerCursor _managerCursor;
        private readonly IOperationLayer _operationLayer;
        private readonly IOptions<OperationOptions> _operationOptions;
        private readonly IMapSettingsController _mapSettingsController;
        private readonly ISelectableController _selectableController;
        private readonly ISelectableObjects _selectableObjects;

        private OperationOptions OperationOptions => _operationOptions.Value;
        private MapSettings MapSettings => _mapSettingsController.Value;
        private float SelectRadius => _operationOptions.Value.SelectRadius;
        private float SelectRadiusMap => CalculatePointHelper.ToMapDistance(MapSettings, SelectRadius);

        private bool _selectRectangle;

        public SelectOperation(
            IManagerCursor managerCursor,
            IOperationLayer operationLayer,
            IOptions<OperationOptions> operationOptions,
            IMapSettingsController mapSettingsController,
            ISelectableController selectableController,
            ISelectableObjects selectableObjects,
            ILogger<SelectOperation> logger = null
        )
            : base(true, logger)
        {
            _managerCursor = managerCursor;
            _operationLayer = operationLayer;
            _operationOptions = operationOptions;
            _mapSettingsController = mapSettingsController;
            _selectableController = selectableController;
            _selectableObjects = selectableObjects;
        }

        protected override bool OnStart(object[] args)
        {
            return true;
        }

        protected override bool OnChange(object[] args)
        {
            if (!_selectRectangle)
            {
                _selectRectangle = SKPoint.Distance(_startPointControl, _currentPointControl) >= OperationOptions.RectangleToleranceRadius;
                if (_selectRectangle)
                {
                    _managerCursor.SetCursor(CursorType.ArrowSelectMany);
                    _operationLayer.AddDraw(DrawRect);
                }
            }

            if (_selectRectangle)
            {
                _operationLayer.Invalidate();
            }

            return true;
        }

        protected override bool OnEnd(object[] args)
        {
            _operationLayer.RemoveDraw(DrawRect);
            if (!_selectRectangle)
            {
                var intersectObjects = _selectableObjects
                    .GetElements(_currentPointMap, SelectRadiusMap);
                if (Keyboard.Modifiers.HasFlag(ModifierKeys.Shift))
                {
                    var selectable = _selectableController.Value
                        .ToArray();
                    if (selectable.Any(v => intersectObjects.Contains(v)))
                    {
                        selectable = selectable
                            .Where(v => !intersectObjects.Contains(v))
                            .ToArray();
                    }
                    else
                    {
                        selectable = selectable
                            .Concat(intersectObjects.Take(1))
                            .Distinct()
                            .ToArray();
                    }
                    _selectableController.Select(selectable);
                }
                else
                {
                    var selectedObject = _selectableController.Value.FirstOrDefault();

                    if (!intersectObjects.Any() || intersectObjects.Count == 1 && intersectObjects.First() == selectedObject)
                    {
                        _selectableController.ClearSelect();
                    }
                    else
                    {
                        var index = intersectObjects.IndexOf(selectedObject);
                        if (index == -1 || intersectObjects.Count == index + 1)
                        {
                            _selectableController.Select(intersectObjects.First());
                        }
                        else
                        {
                            _selectableController.Select(intersectObjects.ElementAt(index + 1));
                        }
                    }
                }
            }
            else
            {
                var intersectObjects = _selectableObjects.GetElements(SelectRect());
                if (Keyboard.Modifiers.HasFlag(ModifierKeys.Shift))
                {
                    var selectable = _selectableController.Value;
                    if (intersectObjects.All(v => selectable.Contains(v)))
                    {
                        selectable = selectable
                            .Where(v => !intersectObjects.Contains(v))
                            .ToArray();
                    }
                    else
                    {
                        selectable = selectable
                            .Concat(intersectObjects)
                            .Distinct()
                            .ToArray();
                    }

                    _selectableController.Select(selectable);
                }
                else
                {
                    if (!intersectObjects.Any())
                    {
                        _selectableController.ClearSelect();
                    }
                    else
                    {
                        _selectableController.Select(intersectObjects);
                    }
                }
            }

            _managerCursor.SetCursor(CursorType.Arrow);
            _selectRectangle = false;
            return true;
        }

        protected override bool OnStop(SKPoint controlPoint, SKPoint mapPoint, object[] args)
        {
            _managerCursor.SetCursor(CursorType.Arrow);
            return true;
        }

        private SKRect SelectRect()
        {
            var rect = new SKRect(
                _startPointMap.X.Min(_currentPointMap.X),
                _startPointMap.Y.Min(_currentPointMap.Y),
                _currentPointMap.X.Max(_startPointMap.X),
                _currentPointMap.Y.Max(_startPointMap.Y)
            );
            return rect;
        }

        private void DrawRect(SKCanvas canvas, SKRect drawrect)
        {
            var rect = SelectRect();
            var points = new[]
            {
                new SKPoint(rect.Left, rect.Top),
                new SKPoint(rect.Right, rect.Top),
                new SKPoint(rect.Right, rect.Bottom),
                new SKPoint(rect.Left, rect.Bottom),
                new SKPoint(rect.Left, rect.Top),
            };
            var firstPoint = points.First();

            var skPathEffect = SKPathEffect.CreateDash(
                new float[] { 3f * 1f / MapSettings.Scale, 3f * 1f / MapSettings.Scale },
                6f * 1f / MapSettings.Scale
            );
            using var paint = new SKPaint
            {
                Color = new SKColor(0, 255, 0),
                PathEffect = skPathEffect,
            };
            Enumerable
                .Range(1, points.Length - 1)
                .ForEach(
                    index =>
                    {
                        canvas.DrawLine(firstPoint, points[index], paint);
                        firstPoint = points[index];
                    }
                );
        }

        //float[] GetPickerArray(Picker picker)
        //{
        //    if (picker.SelectedIndex == -1)
        //    {
        //        return new float[0];
        //    }

        //    string str = (string)picker.SelectedItem;
        //    string[] strs = str.Split(new char[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries);
        //    float[] array = new float[strs.Length];

        //    for (int i = 0; i < strs.Length; i++)
        //    {
        //        array[i] = Convert.ToSingle(strs[i]);
        //    }
        //    return array;
        //}
    }
}
