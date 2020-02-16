using System.IO;
using System.Linq;
using System.Reflection;

namespace CourseplayEditor.Tools.Tools
{
    public static class EmbeddedResources
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="assembly"></param>
        /// <param name="name">Имя ресурса.
        /// Format: "{Namespace}.{Folder}.{filename}.{Extension}"
        /// </param>
        /// <returns></returns>
        public static Stream ReadResource(Assembly assembly, string name)
        {
            var manifestResourceNames = assembly.GetManifestResourceNames();
            var resourcePath = manifestResourceNames
                    .Single(str => str.EndsWith(name));

            return assembly.GetManifestResourceStream(resourcePath);
        }

        public static Stream ReadResource<T>(string name)
        {
            return ReadResource(typeof(T).Assembly, name);
        }

        public static Stream ReadResourceExecutingAssembly(string name)
        {
            return ReadResource(Assembly.GetExecutingAssembly(), name);
        }
    }
}
