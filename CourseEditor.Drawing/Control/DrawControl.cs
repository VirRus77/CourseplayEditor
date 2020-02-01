using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using CourseEditor.Drawing.Contract;
using CourseEditor.Drawing.Controllers.Implementation;
using CourseEditor.Drawing.Tools;
using CourseEditor.Drawing.Tools.Extensions;
using SkiaSharp;

namespace CourseEditor.Drawing.Control
{
    /// <summary>
    /// Контрол отображения
    /// </summary>
    public class DrawControl : FrameworkElement, IDrawControl
    {
        private readonly IMapSettingsController _mapSettings;
        private readonly IDrawLayerManager _layerManager;

        /// <inheritdoc cref="Controllers.Implementation.MapSettings"/>
        private MapSettings MapSettings => _mapSettings.Value;

        /// <summary>
        /// <inheritdoc cref="Controllers.Implementation.MapSettings.PointLeftTop"/>
        /// </summary>
        private SKPoint PointLeftTop => MapSettings.PointLeftTop;

        /// <summary>
        /// <inheritdoc cref="Controllers.Implementation.MapSettings.Scale"/>
        /// </summary>
        private float Scale => MapSettings.Scale;

        /// <summary>
        /// Конструктор <inheritdoc cref="DrawControl"/>
        /// </summary>
        public DrawControl()
        {
            SnapsToDevicePixels = true;
        }

        public DrawControl([NotNull]IMapSettingsController mapSettings, [NotNull]IDrawLayerManager layerManager)
            : this()
        {
            _mapSettings = mapSettings ?? throw new ArgumentNullException(nameof(mapSettings));
            _layerManager = layerManager ?? throw new ArgumentNullException(nameof(layerManager));

            _mapSettings.Changed += MapSettingsOnChanged;
            _layerManager.Changed += LayerManagerOnChanged;
        }

        private void LayerManagerOnChanged(object? sender, EventArgs e)
        {
            InvalidateVisual();
        }

        private void MapSettingsOnChanged(object? sender, ValueEventArgs<MapSettings> e)
        {
            InvalidateVisual();
        }

        /// <inheritdoc />
        protected override void OnRender(DrawingContext drawingContext)
        {
            if (ActualWidth < 1 || ActualHeight < 1 || !double.IsNormal(ActualWidth) || !double.IsNormal(ActualHeight))
            {
                return;
            }

            var writeableBitmap = CreateImage((int)ActualWidth, (int)ActualHeight);
            int width = (int)writeableBitmap.Width,
                height = (int)writeableBitmap.Height;
            writeableBitmap.Lock();

            var skImageInfo = new SKImageInfo
            {
                Width = width,
                Height = height,
                ColorType = SKColorType.Bgra8888,
                AlphaType = SKAlphaType.Premul
            };

            using var surface = SKSurface.Create(
                skImageInfo,
                pixels: writeableBitmap.BackBuffer,
                rowBytes: width * 4
            );
            using var canvas = surface.Canvas;
            canvas.Translate(PointLeftTop.Mult(-1).Mult(Scale));
            canvas.Scale(Scale);

            Draw(canvas, width, height);

            writeableBitmap.AddDirtyRect(new Int32Rect(0, 0, width, height));
            writeableBitmap.Unlock();
            drawingContext.DrawImage(writeableBitmap, new Rect(new Size(width, height)));
        }

        private void Draw(SKCanvas canvas, int width, int height)
        {
            var pointMapLT = CalculatePointHelper.ToMapPoint(MapSettings, new SKPoint(0, 0));
            var pointMapRB = CalculatePointHelper.ToMapPoint(MapSettings, new SKPoint(width, height));

            var drawRect = new SKRect(
                pointMapLT.X,
                pointMapLT.Y,
                pointMapRB.X,
                pointMapRB.Y
            );

            _layerManager.Layers
                         .ToList()
                         .ForEach(v => v.Draw(canvas, drawRect));
        }

        private WriteableBitmap CreateImage(int width, int height)
        {
            return new WriteableBitmap(width, height, 96, 96, PixelFormats.Bgra32, BitmapPalettes.Halftone256Transparent);
        }
    }
}
