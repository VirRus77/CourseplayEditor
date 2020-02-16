using System;
using System.Collections.Generic;
using System.Linq;
using CourseEditor.Drawing.Contract;
using CourseEditor.Drawing.Contract.Operations;
using Microsoft.Extensions.Logging;
using SkiaSharp;

namespace CourseEditor.Drawing.Implementation.Operations
{
    public class MoveOperation : BaseOperation, IMoveOperation
    {
        private readonly IManagerCursor _managerCursor;
        private readonly IOperationLayer _operationLayer;
        private readonly ISelectableController _selectableController;

        private ICollection<ISelectable> _moveObjects;

        public MoveOperation(
            IManagerCursor managerCursor,
            IOperationLayer operationLayer,
            ISelectableController selectableController,
            ILogger<MoveOperation> logger = null
        )
            : base(true, logger)
        {
            _managerCursor = managerCursor;
            _operationLayer = operationLayer;
            _selectableController = selectableController;
        }

        protected override bool OnStart(object[] args)
        {
            _managerCursor.SetCursor(CursorType.ArrowMove);
            _moveObjects = _selectableController.Value;
            _operationLayer.AddDraw(MoveDraw);
            return true;
        }

        protected override bool OnChange(object[] args)
        {
            _operationLayer.Invalidate();
            return true;
        }

        protected override bool OnEnd(object[] args)
        {
            _operationLayer.RemoveDraw(MoveDraw);
            return true;
        }

        protected override bool OnStop(SKPoint controlPoint, SKPoint mapPoint, object[] args)
        {
            _operationLayer.RemoveDraw(MoveDraw);
            return true;
        }

        private void MoveDraw(SKCanvas canvas, SKRect drawrect)
        {
            if (_moveObjects?.Any() != true)
            {
                return;
            }

            
        }
    }
}
