using CourseEditor.Drawing.Contract.Operations;
using Microsoft.Extensions.Logging;
using SkiaSharp;

namespace CourseEditor.Drawing.Implementation.Operations
{
    public abstract class BaseOperation : IOperation
    {
        protected readonly ILogger _logger;
        protected SKPoint _startPointControl;
        protected SKPoint _startPointMap;
        protected SKPoint _deltaPointControl;
        protected SKPoint _deltaPointMap;
        protected SKPoint _currentPointControl;
        protected SKPoint _currentPointMap;

        protected SKPoint DeltaPointControl => _deltaPointControl;
        protected SKPoint DeltaPointMap => _deltaPointMap;

        protected BaseOperation(bool isDelayOperation, ILogger logger = null)
        {
            _logger = logger;
            IsDelayOperation = isDelayOperation;
        }

        public bool IsDelayOperation { get; }

        public bool IsRun { get; private set; }

        public virtual bool Start(SKPoint controlPoint, SKPoint mapPoint, params object[] args)
        {
            _startPointControl = controlPoint;
            _startPointMap = mapPoint;
            _deltaPointControl = SKPoint.Empty;
            _deltaPointMap = SKPoint.Empty;
            _currentPointControl = controlPoint;
            _currentPointMap = mapPoint;

            var isStart = OnStart(args);
            if (IsDelayOperation)
            {
                IsRun = isStart;
            }

            return isStart;
        }

        public bool Change(SKPoint controlPoint, SKPoint mapPoint, params object[] args)
        {
            _deltaPointControl = controlPoint - _currentPointControl;
            _deltaPointMap = mapPoint - _currentPointMap;
            _currentPointControl = controlPoint;
            _currentPointMap = mapPoint;
            IsRun = !OnChange(args);
            _logger.LogInformation("IsRun: {IsRun}", IsRun);
            return IsRun;
        }

        public virtual bool End(SKPoint controlPoint, SKPoint mapPoint, params object[] args)
        {
            _currentPointControl = controlPoint;
            _currentPointMap = mapPoint;
            IsRun = !OnEnd(args);
            return IsRun;
        }

        public virtual bool Stop(SKPoint controlPoint, SKPoint mapPoint, params object[] args)
        {
            IsRun = !OnStop(controlPoint, mapPoint, args);
            return IsRun;
        }


        protected abstract bool OnStart(object[] args);
        protected abstract bool OnChange(object[] args);
        protected abstract bool OnEnd(object[] args);
        protected abstract bool OnStop(SKPoint controlPoint, SKPoint mapPoint, object[] args);
    }
}
