using System;
using System.Linq;
using System.Windows.Input;
using Core.Tools.Extensions;
using CourseEditor.Drawing.Contract;
using CourseEditor.Drawing.Controllers.Implementation;
using CourseEditor.Drawing.Tools;
using SkiaSharp;

namespace CourseEditor.Drawing.Controllers.Mouse
{
    public class MouseOperationSelect : BaseMouseOperation
    {
        private const float _selectRadius = 5;

        private readonly IMapSettingsController _mapSettingsController;
        private readonly ISelectableController _selectableController;
        private readonly ISelectableObjects _selectableObjects;

        private MapSettings MapSettings => _mapSettingsController.Value;

        public MouseOperationSelect(
            IMapSettingsController mapSettingsController,
            ISelectableController selectableController,
            ISelectableObjects selectableObjects
        )
            : base(MouseOperationType.Select, MouseEventType.Move | MouseEventType.Up | MouseEventType.Down)
        {
            _mapSettingsController = mapSettingsController;
            _selectableController = selectableController;
            _selectableObjects = selectableObjects;
        }

        protected void Delta(SKPoint currentPoint)
        {
            var delta = LastDeltaPoint - currentPoint;
            LastDeltaPoint = currentPoint;

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
            return true;
        }

        public override bool OnMouseMove(MouseEventArgs mouseEventArgs, SKPoint controlPosition)
        {
            if (!IsRun)
            {
                return false;
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
            var radius = CalculatePointHelper.ToMapDistance(MapSettings, _selectRadius);

            var selectedObjects = _selectableController.Value.FirstOrDefault();
            var intersectObjects = _selectableObjects.GetElements(mapPoint, radius);

            if (!intersectObjects.Any())
            {
                _selectableController.ClearSelect();
            }
            else
            {
                var index = intersectObjects.IndexOf(selectedObjects);
                if (index == -1 || intersectObjects.Count == index + 1)
                {
                    _selectableController.Select(intersectObjects.First());
                }
                else
                {
                    _selectableController.Select(intersectObjects.ElementAt(index + 1));
                }
            }


            End(controlPosition);
            return true;
        }
    }
}
