using System;
using CourseplayEditor.Tools.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace CourseplayEditor.Tools
{
    /// <summary>
    /// Базовый класс для облегчения создания DependencyInjection.
    /// </summary>
    public abstract class BaseDependencyInjection : IDisposable
    {
        private IServiceCollection _serviceCollection;
        private IConfigurationBuilder _configurationBuilder;

        protected BaseDependencyInjection()
        {
            _serviceCollection = CreateServiceCollection();
            _configurationBuilder = CreateConfigurationBuilder();
        }

        /// <summary>
        /// Провайдер сервисов, доступен только после <see cref="Build"/>.
        /// </summary>
        public IServiceProvider ServiceProvider { get; private set; }

        /// <summary>
        /// Загруженная конфигурация
        /// </summary>
        public IConfigurationRoot Configuration { get; private set; }

        /// <summary>
        /// Логгер доступен только после <see cref="Build"/>.
        /// </summary>
        public ILogger Logger { get; private set; }

        /// <summary>
        /// Настраиваем логирования.
        /// </summary>
        protected virtual void ConfigureLogging(ILoggingBuilder builder, IConfigurationRoot configurationRoot)
        {
        }

        /// <summary>
        /// Конфигурируем основные настройки
        /// </summary>
        /// <param name="configurationBuilder">Конфигуратор настроек</param>
        protected abstract void Configure(IConfigurationBuilder configurationBuilder);

        /// <summary>
        /// Конфигурируем сервисы
        /// </summary>
        /// <param name="serviceCollection">Коллекция сервисов</param>
        protected abstract void Configure(IServiceCollection serviceCollection);

        /// <summary>
        /// Настройка сервисов после загрузки конфигурации
        /// </summary>
        /// <param name="serviceCollection"></param>
        /// <param name="configurationRoot"></param>
        protected virtual void Configure(IServiceCollection serviceCollection, IConfigurationRoot configurationRoot)
        {
        }

        /// <summary>
        /// Создаем DI и <see cref="ServiceProvider"/> по настройкам
        /// </summary>
        protected virtual void Build()
        {
            Configure(_configurationBuilder);
            Configuration = _configurationBuilder.Build();

            _serviceCollection
                .AddLogging(builder => ConfigureLogging(builder, Configuration));

            _serviceCollection
                .AddConfiguration(Configuration);

            Configure(_serviceCollection);
            Configure(_serviceCollection, Configuration);

            ServiceProvider = _serviceCollection.BuildServiceProvider();

            Logger = ServiceProvider.GetService<ILogger<BaseDependencyInjection>>();
        }

        /// <summary>
        /// Создаем <see cref="IConfigurationBuilder"/>
        /// </summary>
        /// <returns></returns>
        private IConfigurationBuilder CreateConfigurationBuilder()
        {
            var configurationBuilder = new ConfigurationBuilder();

            return configurationBuilder;
        }

        /// <summary>
        /// Создаем <see cref="IServiceCollection"/>
        /// </summary>
        /// <returns></returns>
        private IServiceCollection CreateServiceCollection()
        {
            var serviceCollection = new ServiceCollection();

            return serviceCollection;
        }

        public virtual void Dispose()
        {
            if (ServiceProvider is IDisposable disposable)
            {
                disposable.Dispose();
            }
        }
    }
}
