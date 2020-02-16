using System;
using System.Linq;
using CourseEditor.Drawing.Contract;
using CourseEditor.Drawing.Contract.Operations;
using SkiaSharp;

namespace CourseEditor.Drawing.Implementation.Operations
{
    public class MapZoomOperation : BaseOperation, IMapZoomOperation
    {
        private readonly IMapSettingsController _mapSettingsController;

        public MapZoomOperation(IMapSettingsController mapSettingsController)
            : base(false)
        {
            _mapSettingsController = mapSettingsController;
        }

        protected override bool OnStart(object[] args)
        {
            if (args?.Any() != true)
            {
                throw new ArgumentException(nameof(args), "Dont have delta zoom.");
            }
            else if (args.Length != 1)
            {
                throw new ArgumentException(nameof(args), "Dont have delta zoom.");
            }
            else if (!(args[0] is int))
            {
                throw new ArgumentException(nameof(args), $"Delta zoom not int: {args[0].GetType().Name}");
            }

            return Zoom(_currentPointControl, _currentPointMap, (int)args[0]);
        }

        protected override bool OnChange(object[] controlPoint)
        {
            throw new NotImplementedException();
        }

        protected override bool OnEnd(object[] args)
        {
            throw new NotImplementedException();
        }

        protected override bool OnStop(SKPoint controlPoint, SKPoint mapPoint, object[] args)
        {
            throw new NotImplementedException();
        }

        public bool Zoom(SKPoint controlPoint, SKPoint mapPoint, int delta)
        {
            _mapSettingsController.ZoomByControlPoint(delta, controlPoint);
            return true;
        }
    }
}
