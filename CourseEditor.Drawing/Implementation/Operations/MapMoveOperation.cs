using CourseEditor.Drawing.Contract;
using CourseEditor.Drawing.Contract.Operations;
using CourseEditor.Drawing.Tools.Extensions;
using Microsoft.Extensions.Logging;
using SkiaSharp;

namespace CourseEditor.Drawing.Implementation.Operations
{
    public class MapMoveOperation : BaseOperation, IMapMoveOperation
    {
        private readonly IManagerCursor _managerCursor;
        private readonly IMapSettingsController _mapSettingsController;

        public MapMoveOperation(
            IManagerCursor managerCursor,
            IMapSettingsController mapSettingsController,
            ILogger<MapMoveOperation> logger
        )
            : base(true, logger)
        {
            _managerCursor = managerCursor;
            _mapSettingsController = mapSettingsController;
        }

        protected override bool OnStart(object[] args)
        {
            _managerCursor.SetCursor(CursorType.Grabbing);
            return true;
        }

        protected override bool OnChange(object[] controlPoint)
        {
            _logger?.LogInformation("DeltaPointControl: {DeltaPointControl}", DeltaPointControl);
            _mapSettingsController.OffsetByControlPoint(DeltaPointControl.Mult(-1));
            return true;
        }

        protected override bool OnEnd(object[] args)
        {
            _managerCursor.SetCursor(CursorType.Arrow);
            return true;
        }

        protected override bool OnStop(SKPoint controlPoint, SKPoint mapPoint, object[] args)
        {
            _mapSettingsController.OffsetByMapPoint(_startPointMap);
            _managerCursor.SetCursor(CursorType.Arrow);
            return true;
        }
    }
}
