using System;
using System.Linq;
using System.Windows.Input;
using Core.Tools.Extensions;
using CourseEditor.Drawing.Contract;
using CourseEditor.Drawing.Controllers.Implementation;
using CourseEditor.Drawing.Implementation;
using CourseEditor.Drawing.Implementation.Configuration;
using CourseEditor.Drawing.Tools;
using CourseEditor.Drawing.Tools.Extensions;
using Microsoft.Extensions.Options;
using SkiaSharp;

namespace CourseEditor.Drawing.Controllers.Mouse
{
    public class MouseOperationSelect : BaseMouseOperation
    {
        private readonly IOptions<OperationOptions> _drawingOptions;
        private readonly IServiceProvider _serviceProvider;
        private readonly IMapSettingsController _mapSettingsController;
        private readonly ISelectableController _selectableController;
        private readonly ISelectableObjects _selectableObjects;
        private readonly IMouseOperation _mouseOperationMove;

        private MapSettings MapSettings => _mapSettingsController.Value;
        private float SelectRadius => _drawingOptions.Value.SelectRadius;
        private float MoveToleranceRadius => _drawingOptions.Value.RectangleToleranceRadius;

        private IMouseOperation _subOperation;
        private bool _canMove;

        public MouseOperationSelect(
            IOptions<OperationOptions> drawingOptions,
            IServiceProvider serviceProvider,
            IMapSettingsController mapSettingsController,
            ISelectableController selectableController,
            ISelectableObjects selectableObjects
        )
            : base(MouseOperationType.Select, MouseEventType.Move | MouseEventType.Up | MouseEventType.Down)
        {
            _drawingOptions = drawingOptions;
            _serviceProvider = serviceProvider;
            _mapSettingsController = mapSettingsController;
            _selectableController = selectableController;
            _selectableObjects = selectableObjects;

            _mouseOperationMove = _serviceProvider.CreateInstance<MouseOperationMove>();
        }

        protected void Delta(SKPoint currentPoint)
        {
            var delta = LastDeltaPoint - currentPoint;
            var destantion = SKPoint.Distance(LastDeltaPoint, currentPoint);
            LastDeltaPoint = currentPoint;

            if (destantion >= MoveToleranceRadius && _canMove)
            {
                _subOperation = _mouseOperationMove;
            }

            //_mapSettingsController.OffsetByControlPoint(delta);
        }

        public override bool OnMouseDown(MouseButtonEventArgs mouseButtonEventArgs, SKPoint controlPosition)
        {
            if (mouseButtonEventArgs.ChangedButton != MouseButton.Left ||
                mouseButtonEventArgs.ButtonState != MouseButtonState.Pressed)
            {
                return false;
            }

            Start(controlPosition);
            _canMove = CanMove(controlPosition);

            return true;
        }

        private bool CanMove(SKPoint controlPosition)
        {
            var mapPoint = CalculatePointHelper.ToMapPoint(MapSettings, controlPosition);
            var radius = CalculatePointHelper.ToMapDistance(_mapSettingsController.Value, SelectRadius);
            var elements = _selectableObjects.GetElements(mapPoint, radius);
            return !elements.Any();
        }

        public override bool OnMouseMove(MouseEventArgs mouseEventArgs, SKPoint controlPosition)
        {
            if (!IsRun)
            {
                return false;
            }

            if (_subOperation != null)
            {
                var onSuboperationMouseMove = _subOperation.OnMouseMove(mouseEventArgs, controlPosition);
                Delta(controlPosition);
                if (!onSuboperationMouseMove)
                {
                    End(controlPosition);
                }
                return onSuboperationMouseMove;
            }

            Delta(controlPosition);
            return true;
        }

        public override bool OnMouseUp(MouseButtonEventArgs mouseButtonEventArgs, SKPoint controlPosition)
        {
            if (!IsRun ||
                mouseButtonEventArgs.ChangedButton != MouseButton.Left ||
                mouseButtonEventArgs.ButtonState != MouseButtonState.Released)
            {
                return false;
            }

            var mapPoint = CalculatePointHelper.ToMapPoint(MapSettings, controlPosition);
            var radius = CalculatePointHelper.ToMapDistance(MapSettings, SelectRadius);

            var selectedObject = _selectableController.Value.FirstOrDefault();
            var intersectObjects = _selectableObjects.GetElements(mapPoint, radius);

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

            _subOperation?.Stop();
            End(controlPosition);
            return true;
        }
    }
}
