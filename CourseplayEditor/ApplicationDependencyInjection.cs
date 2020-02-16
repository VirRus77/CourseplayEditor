using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Input;
using CourseEditor.Drawing;
using CourseEditor.Drawing.Contract;
using CourseEditor.Drawing.Contract.Configuration;
using CourseEditor.Drawing.Contract.Operations;
using CourseEditor.Drawing.Control;
using CourseEditor.Drawing.Controllers;
using CourseEditor.Drawing.Controllers.Implementation;
using CourseEditor.Drawing.Controllers.Mouse;
using CourseEditor.Drawing.Implementation;
using CourseEditor.Drawing.Implementation.Configuration;
using CourseEditor.Drawing.Implementation.Operations;
using CourseplayEditor.Configuration;
using CourseplayEditor.Contracts;
using CourseplayEditor.Implementation;
using CourseplayEditor.Implementation.Control;
using CourseplayEditor.Implementation.Layers;
using CourseplayEditor.Tools;
using CourseplayEditor.Tools.Extensions;
using CourseplayEditor.Tools.Tools;
using CourseplayEditor.View;
using CourseplayEditor.ViewModel;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SkiaSharp;
using CursorType = CourseEditor.Drawing.Contract.CursorType;

namespace CourseplayEditor
{
    public class ApplicationDependencyInjection : BaseDependencyInjection
    {
        public ApplicationDependencyInjection()
        {
            Build();
        }

        protected override void Configure(IConfigurationBuilder configurationBuilder)
        {
            configurationBuilder
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", false, true);
        }

        protected override void ConfigureLogging(ILoggingBuilder builder, IConfigurationRoot configurationRoot)
        {
            builder
                .AddConsole()
                .SetMinimumLevel(LogLevel.Trace);
        }

        protected override void Configure(IServiceCollection serviceCollection, IConfigurationRoot configurationRoot)
        {
            serviceCollection
                .AddConfiguration(configurationRoot)
                //.AddSingleton<ApplicationConfiguration>(
                //    provider => configurationRoot.GetSection("ApplicationConfiguration").Get<ApplicationConfiguration>()
                //)
                //.Configure<ApplicationConfiguration>(options => configurationRoot.GetSection("ApplicationConfiguration").Bind(options))
                .Configure<OperationOptions>(
                    configurationRoot.GetSection($"{nameof(ApplicationConfiguration)}:{nameof(ApplicationConfiguration.OperationOptions)}")
                );
            //.Configure<OperationOptions>(options => configurationRoot.GetSection("ApplicationConfiguration:OperationOptions").Bind(options));
        }

        protected override void Configure(IServiceCollection serviceCollection)
        {
            serviceCollection
                .AddSingleton<IMainWindow>(provider => new MainWindow(provider.GetService<MainWindowViewModel>()))
                .AddSingleton<MainWindowViewModel>()
                .AddSingleton<IDrawControl, DrawControl>(
                    provider => provider.CreateInstance<DrawControl>(provider.GetService<IDrawLayerManager>())
                )
                .AddSingleton<IDrawLayerManager>(
                    provider =>
                    {
                        var manager = new DrawLayerManager();
                        //manager.AddLayer(new TestDrawLayer());
                        return manager;
                    }
                )
                .AddSingleton<IOperationLayer, OperationLayer>()
                .AddSingleton<ICourseLayerManager, CourseLayerManager>()
                .AddSingleton<MouseController>(
                    provider =>
                    {
                        var mouseController = provider.CreateInstance<MouseController>();
                        mouseController.Init((FrameworkElement)provider.GetService<IDrawControl>());
                        return mouseController;
                    }
                )
                .AddSingleton<ICurrentPositionController, CurrentPositionController>()
                .AddSingleton<ISelectableController, SelectableController>()
                .AddSingleton<IMapSettingsController, MapSettingsController>()
                .AddSingleton<IMouseOperationManager, MouseOperationManager>()
                .AddSingleton<SelectableObjects.IModelSelectableObjects, SelectableObjects>()
                .AddSingleton<ISelectableObjects>(provider => provider.GetService<SelectableObjects.IModelSelectableObjects>())
                .AddSingleton<IDrawCurrentPosition, DrawCurrentPosition>()
                .AddSingleton<IChangeProvider, ChangeProvider>()
                .AddSingleton<IManagerCursor>(
                    provider =>
                    {
                        return new ManagerCursor(
                            new Dictionary<CursorType, Cursor>
                            {
                                {
                                    CursorType.Arrow,
                                    new Cursor(EmbeddedResources.ReadResource<ApplicationDependencyInjection>("Arrow.cur"))
                                },
                                {
                                    CursorType.ArrowMove,
                                    new Cursor(EmbeddedResources.ReadResource<ApplicationDependencyInjection>("ArrowMove.cur"))
                                },
                                {
                                    CursorType.ArrowAdd,
                                    new Cursor(EmbeddedResources.ReadResource<ApplicationDependencyInjection>("ArrowAdd.cur"))
                                },
                                {
                                    CursorType.ArrowSelectMany,
                                    new Cursor(EmbeddedResources.ReadResource<ApplicationDependencyInjection>("ArrowRectangle.cur"))
                                },
                                {
                                    CursorType.Grabbing,
                                    new Cursor(EmbeddedResources.ReadResource<ApplicationDependencyInjection>("Grabbing.cur"))
                                },
                            }
                        );
                    }
                )
                .AddSingleton<IMapMoveOperation, MapMoveOperation>()
                .AddSingleton<IMapZoomOperation, MapZoomOperation>()
                .AddSingleton<ISelectOperation, SelectOperation>()
                .AddSingleton<IMoveOperation, MoveOperation>()
                .AddSingleton<ICalculateHelper, CalculateHelper>()
                .AddSingleton<IManagedDrawSelectableObject, DrawSelectableObject>()
                .AddSingleton<IDrawConfiguration>(
                    provider =>
                    {
                        var drawConfiguration = new DrawConfiguration();
                        drawConfiguration.Add(
                            CourseDrawLayer.DrawCourseLayerKey,
                            new SKPaint
                            {
                                Color = new SKColor(0, 255, 0)
                            }
                        );
                        drawConfiguration.AddGradient(
                            CourseDrawLayer.DrawCourseLayerKey,
                            new SKPaint
                            {
                                Color = new SKColor(0, 255, 0)
                            },
                            new SKPaint
                            {
                                Color = new SKColor(255, 255, 0)
                            },
                            new SKPaint
                            {
                                Color = new SKColor(255, 0, 0)
                            }
                        );
                        drawConfiguration.Add(
                            OperationLayer.SelectableItemsKey,
                            new SKPaint
                            {
                                Color = new SKColor(0, 255, 33)
                            }
                        );
                        drawConfiguration.Add(
                            SplineDrawLayer.SplineDrawLayerKey,
                            new SKPaint
                            {
                                Color = new SKColor(214, 127, 255)
                            }
                        );
                        return drawConfiguration;
                    }
                );

            serviceCollection
                .AddSingleton<ICourseFile, CourseFile>();
        }
    }
}
