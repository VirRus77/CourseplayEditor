using System;
using Microsoft.Extensions.DependencyInjection;

namespace CourseEditor.Drawing.Tools.Extensions
{
    public static class ServiceProviderExtensions
    {
        public static T CreateInstance<T>(this IServiceProvider serviceProvider, params object[] parameters)
        {
            return ActivatorUtilities.CreateInstance<T>(serviceProvider, parameters);
        }
    }
}
