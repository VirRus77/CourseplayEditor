using System.Windows;
using CourseEditor.Drawing;
using CourseEditor.Drawing.Contract;
using CourseEditor.Drawing.Control;
using CourseEditor.Drawing.Controllers;
using CourseEditor.Drawing.Controllers.Mouse;
using CourseEditor.Drawing.Implementation;
using CourseplayEditor.Configuration;
using CourseplayEditor.Contracts;
using CourseplayEditor.Implementation;
using CourseplayEditor.Implementation.Control;
using CourseplayEditor.Implementation.Layers;
using CourseplayEditor.Tools;
using CourseplayEditor.Tools.Extensions;
using CourseplayEditor.View;
using CourseplayEditor.ViewModel;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

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
                .AddJsonFile("appsettings.json");
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
                .AddSingleton<ApplicationConfiguration>(
                    provider => configurationRoot.GetSection("ApplicationConfiguration").Get<ApplicationConfiguration>()
                );
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
                .AddSingleton<OperationLayer>()
                .AddSingleton<ICourseLayerManager, CourseLayerManager>()
                .AddSingleton<MouseController>(
                    provider =>
                    {
                        var mouseController = provider.CreateInstance<MouseController>();
                        mouseController.Init((FrameworkElement) provider.GetService<IDrawControl>());
                        return mouseController;
                    }
                )
                .AddSingleton<ICurrentPositionController, CurrentPositionController>()
                .AddSingleton<ISelectableController, SelectableController>()
                .AddSingleton<IMapSettingsController, MapSettingsController>()
                .AddSingleton<IMouseOperationManager, MouseOperationManager>()
                .AddSingleton<IDrawCurrentPosition, DrawCurrentPosition>();

            serviceCollection
                .AddSingleton<ICourseFile, CourseFile>();
        }
    }
}
