using System;
using Microsoft.Extensions.DependencyInjection;

namespace CourseplayEditor.Tools.Extensions
{
    public static class ServiceProviderExtensions
    {
        public static T CreateInstance<T>(this IServiceProvider serviceProvider, params object[] args)
        {
            return (T) serviceProvider.CreateInstance(typeof(T), args);
        }

        public static object CreateInstance(this IServiceProvider serviceProvider, Type instanceType, params object[] args)
        {
            return ActivatorUtilities.CreateInstance(serviceProvider, instanceType, args);
        }
    }
}
