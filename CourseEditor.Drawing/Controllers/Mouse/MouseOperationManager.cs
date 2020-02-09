using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using CourseEditor.Drawing.Contract;
using CourseEditor.Drawing.Tools.Extensions;
using Microsoft.Extensions.Logging;
using SkiaSharp;

namespace CourseEditor.Drawing.Controllers.Mouse
{
    public class MouseOperationManager : IMouseOperationManager
    {
        private readonly ICurrentPositionController _currentPositionController;
        private readonly ILogger<MouseOperationManager> _logger;
        private readonly ICollection<IMouseOperation> _mouseOperations;

        public MouseOperationManager(
            IServiceProvider serviceProvider,
            ICurrentPositionController currentPositionController,
            ILogger<MouseOperationManager> logger
        )
        {
            _currentPositionController = currentPositionController;
            _logger = logger;

            _mouseOperations = new List<IMouseOperation>
            {
                serviceProvider.CreateInstance<MouseOperationSelect>(),
                serviceProvider.CreateInstance<MouseOperationMapMove>(),
                serviceProvider.CreateInstance<MouseOperationMapZoom>(),
            };
        }

        public IMouseOperation CurrentMouseOperation { get; private set; }

        public void StopOperation()
        {
            CurrentMouseOperation?.Stop();
        }

        public bool OnMouseMove(MouseEventArgs mouseEventArgs, SKPoint controlPosition)
        {
            _currentPositionController.SetValue(controlPosition);
            SetCurrentMouseOperation(
                new[]
                    {
                        CurrentMouseOperation
                    }
                    .Concat(_mouseOperations)
                    .Where(v => v != null)
                    .Where(v => v.SupportMouseEvent.HasFlag(MouseEventType.Move))
                    .SkipWhile(operation => !operation.OnMouseMove(mouseEventArgs, controlPosition))
                    .FirstOrDefault()
            );

            return CurrentMouseOperation?.IsRun == true;
        }

        public bool OnMouseUp(MouseButtonEventArgs mouseButtonEventArgs, SKPoint controlPosition)
        {
            CurrentMouseOperation =
                new[]
                    {
                        CurrentMouseOperation
                    }
                    .Concat(_mouseOperations)
                    .Where(v => v != null)
                    .Where(v => v.SupportMouseEvent.HasFlag(MouseEventType.Up))
                    .SkipWhile(operation => !operation.OnMouseUp(mouseButtonEventArgs, controlPosition))
                    .FirstOrDefault();
            return CurrentMouseOperation?.IsRun == true;
        }

        public bool OnMouseDown(MouseButtonEventArgs mouseButtonEventArgs, SKPoint controlPosition)
        {
            CurrentMouseOperation =
                new[]
                    {
                        CurrentMouseOperation
                    }
                    .Concat(_mouseOperations)
                    .Where(v => v != null)
                    .Where(v => v.SupportMouseEvent.HasFlag(MouseEventType.Down))
                    .SkipWhile(operation => !operation.OnMouseDown(mouseButtonEventArgs, controlPosition))
                    .FirstOrDefault();
            return CurrentMouseOperation?.IsRun == true;
        }

        public bool OnMouseWheel(MouseWheelEventArgs mouseWheelEventArgs, SKPoint controlPosition)
        {
            CurrentMouseOperation =
                new[]
                    {
                        CurrentMouseOperation
                    }
                    .Concat(_mouseOperations)
                    .Where(v => v != null)
                    .Where(v => v.SupportMouseEvent.HasFlag(MouseEventType.Wheel))
                    .SkipWhile(operation => !operation.OnMouseWheel(mouseWheelEventArgs, controlPosition))
                    .FirstOrDefault();
            return CurrentMouseOperation?.IsRun == true;
            //if (_currentMouseOperation != null)
            //{
            //    var onMouseWheel = _currentMouseOperation.OnMouseMove(mouseWheelEventArgs, controlPosition);
            //    if (!onMouseWheel)
            //    {
            //        _currentMouseOperation = null;
            //    }

            //    return onMouseWheel;
            //}

            //_currentMouseOperation = _mouseOperations
            //                         .Where(v => v.SupportMouseEvent.HasFlag(MouseEventType.Wheel))
            //                         .FirstOrDefault(operation => operation.OnMouseWheel(mouseWheelEventArgs, controlPosition));
            //return _currentMouseOperation != null;
        }

        private void SetCurrentMouseOperation(IMouseOperation newMouseOperation)
        {
            if (CurrentMouseOperation != newMouseOperation)
            {
                CurrentMouseOperation?.Stop();
            }

            CurrentMouseOperation = newMouseOperation;
        }
    }
}
