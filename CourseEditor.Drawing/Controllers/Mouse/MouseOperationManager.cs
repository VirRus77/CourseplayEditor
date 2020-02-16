using System;
using System.Linq;
using System.Windows.Input;
using CourseEditor.Drawing.Contract;
using CourseEditor.Drawing.Contract.Operations;
using CourseEditor.Drawing.Controllers.Implementation;
using CourseEditor.Drawing.Implementation;
using CourseEditor.Drawing.Implementation.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SkiaSharp;
using CursorType = CourseEditor.Drawing.Contract.CursorType;

namespace CourseEditor.Drawing.Controllers.Mouse
{
    public class MouseOperationManager : IMouseOperationManager
    {
        private readonly ICalculateHelper _calculateHelper;
        private readonly IOptions<OperationOptions> _operationOptions;
        private readonly IMapSettingsController _mapSettingsController;
        private readonly ICurrentPositionController _currentPositionController;
        private readonly IManagerCursor _managerCursor;
        private readonly ISelectableController _selectableController;
        private readonly ISelectableObjects _selectableObjects;
        private readonly ILogger<MouseOperationManager> _logger;
        private readonly IOperation _activeOperation;

        // Operations
        private readonly IMapZoomOperation _mapZoomOperation;
        private readonly IMapMoveOperation _mapMoveOperation;
        private readonly ISelectOperation _selectOperation;
        private readonly IMoveOperation _moveOperation;
        private bool _canMove;

        private MapSettings MapSettings => _mapSettingsController.Value;

        public MouseOperationManager(
            ICalculateHelper calculateHelper,
            IOptions<OperationOptions> operationOptions,
            IServiceProvider serviceProvider,
            IMapSettingsController mapSettingsController,
            ICurrentPositionController currentPositionController,
            IManagerCursor managerCursor,
            ISelectableController selectableController,
            ISelectableObjects selectableObjects,
            ILogger<MouseOperationManager> logger
        )
        {
            _calculateHelper = calculateHelper;
            _operationOptions = operationOptions;
            _mapSettingsController = mapSettingsController;
            _currentPositionController = currentPositionController;
            _managerCursor = managerCursor;
            _selectableController = selectableController;
            _selectableObjects = selectableObjects;
            _logger = logger;

            _mapZoomOperation = serviceProvider.GetService<IMapZoomOperation>();
            _mapMoveOperation = serviceProvider.GetService<IMapMoveOperation>();
            _selectOperation = serviceProvider.GetService<ISelectOperation>();
            _moveOperation = serviceProvider.GetService<IMoveOperation>();
        }

        public IOperation CurrentOperation { get; private set; }

        public void StopOperation()
        {
            //CurrentMouseOperation?.Stop();
        }

        public void KeyChanged(KeyEventArgs keyEventArgs)
        {
            if (CurrentOperation != null)
            {
                return;
            }

            if (Keyboard.Modifiers.HasFlag(ModifierKeys.Shift))
            {
                _managerCursor.SetCursor(CursorType.Arrow);
            }
            else
            {
                _canMove = CanMove(_currentPositionController.Value);
                if (_canMove)
                {
                    _managerCursor.SetCursor(CursorType.ArrowMove);
                }
            }
        }

        public bool OnMouseMove(MouseEventArgs mouseEventArgs, SKPoint controlPosition)
        {
            CurrentOperation?.Change(controlPosition, ToMapPoint(controlPosition));
            _currentPositionController.SetValue(controlPosition);
            if (CurrentOperation == null)
            {
                _canMove = CanMove(controlPosition);
                if (_canMove)
                {
                    _managerCursor.SetCursor(CursorType.ArrowMove);
                }
            }
            else
            {
                _canMove = false;
            }

            return CurrentOperation != null;
        }

        public bool OnMouseUp(MouseButtonEventArgs mouseButtonEventArgs, SKPoint controlPosition)
        {
            var releaseCurrentOperation = ReleaseCurrentOperation(mouseButtonEventArgs, controlPosition);
            if (releaseCurrentOperation)
            {
                CurrentOperation.End(controlPosition, ToMapPoint(controlPosition));
                SetCurrentMouseOperation(null);
                _canMove = CanMove(controlPosition);

                if (_canMove)
                {
                    _managerCursor.SetCursor(CursorType.ArrowMove);
                }
            }

            return CurrentOperation?.IsRun ?? false;
        }

        public bool OnMouseDown(MouseButtonEventArgs mouseButtonEventArgs, SKPoint controlPosition)
        {
            if (CurrentOperation != null)
            {
                return true;
            }

            var operation = GetStartOperation(mouseButtonEventArgs, controlPosition);
            SetCurrentMouseOperation(operation);
            return operation?.Start(controlPosition, ToMapPoint(controlPosition)) ?? false;
        }

        public bool OnMouseWheel(MouseWheelEventArgs mouseWheelEventArgs, SKPoint controlPosition)
        {
            if (CurrentOperation != null)
            {
                return true;
            }

            var zoomDelta = mouseWheelEventArgs.Delta / System.Windows.Input.Mouse.MouseWheelDeltaForOneLine;
            _mapZoomOperation.Zoom(controlPosition, ToMapPoint(controlPosition), zoomDelta);
            return _mapZoomOperation.IsDelayOperation;
        }

        private bool CanMove(SKPoint controlPosition)
        {
            if (!Keyboard.Modifiers.HasFlag(ModifierKeys.Shift))
            {
                var selectedElements = _selectableController.Value;
                var moveElements = _selectableObjects.GetElements(
                    selectedElements,
                    ToMapPoint(controlPosition),
                    _calculateHelper.ToMapDistance(_operationOptions.Value.MoveToleranceRadius)
                );
                if (moveElements.Any())
                {
                    //_managerCursor.SetCursor(CursorType.ArrowMove);
                    return true;
                }
            }

            _managerCursor.SetCursor(CursorType.Arrow);
            return false;
        }

        private bool ReleaseCurrentOperation(MouseButtonEventArgs mouseButtonEventArgs, SKPoint controlPosition)
        {
            switch (CurrentOperation)
            {
                case IMapMoveOperation mapMoveOperation:
                    return mouseButtonEventArgs.ChangedButton == MouseButton.Middle &&
                           mouseButtonEventArgs.ButtonState == MouseButtonState.Released;
                case ISelectOperation selectOperation:
                    return mouseButtonEventArgs.ChangedButton == MouseButton.Left &&
                           mouseButtonEventArgs.ButtonState == MouseButtonState.Released;
                case IMoveOperation moveOperation:
                    return mouseButtonEventArgs.ChangedButton == MouseButton.Left &&
                           mouseButtonEventArgs.ButtonState == MouseButtonState.Released;
            }

            return false;
        }

        private IOperation GetStartOperation(MouseButtonEventArgs mouseButtonEventArgs, SKPoint controlPosition)
        {
            if (mouseButtonEventArgs.ButtonState == MouseButtonState.Pressed && mouseButtonEventArgs.ChangedButton == MouseButton.Middle)
            {
                return _mapMoveOperation;
            }

            if (mouseButtonEventArgs.ButtonState == MouseButtonState.Pressed && mouseButtonEventArgs.ChangedButton == MouseButton.Left)
            {
                // ReSharper disable RedundantCast
                return CanMove(controlPosition)
                    ? (IOperation)_moveOperation
                    : (IOperation)_selectOperation;
                // ReSharper restore RedundantCast
            }

            return null;
        }

        private IOperation GetOperationChange(MouseButtonEventArgs mouseButtonEventArgs, SKPoint controlPosition)
        {
            if (mouseButtonEventArgs.ButtonState == MouseButtonState.Pressed && mouseButtonEventArgs.ChangedButton == MouseButton.Middle)
            {
                return _mapMoveOperation;
            }

            return null;
        }

        private object GetOperation(MouseButtonEventArgs mouseButtonEventArgs, SKPoint controlPosition)
        {
            if (mouseButtonEventArgs.MiddleButton == MouseButtonState.Pressed)
            {
                return _mapMoveOperation;
            }

            return null;
        }

        private SKPoint ToMapPoint(SKPoint controlPoint)
        {
            return _calculateHelper.ToMapPoint(controlPoint);
        }

        private void SetCurrentMouseOperation(IOperation newMouseOperation)
        {
            CurrentOperation = newMouseOperation;
            switch (CurrentOperation)
            {
                case IMapMoveOperation mapMoveOperation:
                    break;
                case IMapZoomOperation mapZoomOperation:
                    break;
                case IMoveOperation moveOperation:
                    break;
                case ISelectOperation selectOperation:
                    break;
                case null:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(CurrentOperation));
            }
        }
    }
}
