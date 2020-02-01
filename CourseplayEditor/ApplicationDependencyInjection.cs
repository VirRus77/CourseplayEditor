using System.Windows;
using CourseEditor.Drawing;
using CourseEditor.Drawing.Contract;
using CourseEditor.Drawing.Control;
using CourseEditor.Drawing.Controllers;
using CourseEditor.Drawing.Controllers.Contract;
using CourseEditor.Drawing.Controllers.Implementation;
using CourseEditor.Drawing.Implementation;
using CourseplayEditor.Configuration;
using CourseplayEditor.Contracts;
using CourseplayEditor.Controls;
using CourseplayEditor.Controls.Drawing;
using CourseplayEditor.Implementation;
using CourseplayEditor.Implementation.Control;
using CourseplayEditor.Tools;
using CourseplayEditor.Tools.Extensions;
using CourseplayEditor.View;
using CourseplayEditor.ViewModel;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ScaleController = CourseEditor.Drawing.Controllers.Implementation.ScaleController;

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
                        manager.AddLayer(new TestDrawLayer());
                        return manager;
                    }
                )
                .AddSingleton<MouseController>(
                    provider =>
                    {
                        var mouseController = provider.CreateInstance<MouseController>();
                        mouseController.Init((FrameworkElement) provider.GetService<IDrawControl>());
                        return mouseController;
                    }
                )
                .AddSingleton<ICurrentPositionController, CurrentPositionController>()
                .AddSingleton<IMapSettingsController, MapSettingsController>()
                .AddSingleton<IDrawCurrentPosition, DrawCurrentPosition>()
                .AddSingleton<IOffsetController, OffsetController>()
                .AddSingleton<IScaleController, ScaleController>()
                .AddSingleton<IParametersController, ParametersController>();

            serviceCollection
                .AddSingleton<ICourseFile, CourseFile>();
        }
    }
}
