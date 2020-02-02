using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using Core.Tools.Extensions;
using CourseplayEditor.Tools.FarmSimulator;

namespace Courseplay.Tests.v2019
{
    public class GenerateClassesByOpenXmlFile
    {
        public void GenerateCsCode()
        {
            var mapsPath = GamePaths.GetGameMapsPath(FarmSimulatorVersion.FarmingSimulator2019);
            var mapFilePath = Path.Combine(mapsPath, "mapUS.i3d");
            var xml = new XmlDocument();
            using var stream = File.OpenRead(mapFilePath);
            xml.Load(stream);
            var codeClases = ExportCodeClasses(xml.DocumentElement);
            var code = string.Join("\n", codeClases.Select(v => GenerateCsCode(v.Value)));
            var names = string.Join("\n", codeClases.Select(v => $"typeof({UpFirstChar(v.Value.Name)}),"));
        }

        private static string GenerateCsCode(CodeClass codeClass)
        {
            var sb = new StringBuilder();
            sb.AppendLine($"public class {UpFirstChar(codeClass.Name)}");
            sb.AppendLine("{");
            codeClass.Attributes.ForEach(v =>
            {
                sb.AppendLine($"    [XmlAttribute(\"{v}\")]");
                sb.AppendLine($"    public string {UpFirstChar(v)} {{ get; set; }}");
                sb.AppendLine();
            }
            );
            codeClass.Elements.ForEach(v =>
            {
                sb.AppendLine($"    [XmlElement(\"{v}\")]");
                sb.AppendLine($"    public {UpFirstChar(v)} {UpFirstChar(v)} {{ get; set; }}");
                sb.AppendLine();
            }
            );
            codeClass.ArrayElements.ForEach(v =>
            {
                sb.AppendLine($"    [XmlElement(\"{v}\")]");
                sb.AppendLine($"    public {UpFirstChar(v)}[] {UpFirstChar(v)} {{ get; set; }}");
                sb.AppendLine();
            }
            );


            sb.AppendLine("}");

            return sb.ToString();
        }

        private static string UpFirstChar(string value)
        {
            return $"{value.Substring(0, 1).ToUpper()}{value.Substring(1)}";
        }

        private IDictionary<string, CodeClass> ExportCodeClasses(XmlElement element, IDictionary<string, CodeClass> dictionary = null)
        {
            dictionary ??= new Dictionary<string, CodeClass>();

            if (!dictionary.TryGetValue(element.Name, out var codeClass))
            {
                codeClass = new CodeClass(element.Name);
                dictionary.Add(element.Name, codeClass);
            }

            ExportAttributes(element, codeClass);
            ExportElements(element, codeClass, dictionary);

            return dictionary;
        }

        private void ExportElements(XmlElement element, CodeClass codeClass, IDictionary<string, CodeClass> dictionary)
        {
            var child = element
                        .ChildNodes
                        .OfType<XmlElement>()
                        .ToArray();
            child.ForEach(v => ExportCodeClasses(v, dictionary));
            var group = child
                        .GroupBy(v => v.Name)
                        .ToArray();

            codeClass.AddArrayElements(group.Where(v => v.Count() > 1).Select(v => v.Key));
            codeClass.AddElements(group.Where(v => v.Count() == 1).Select(v => v.Key));
        }

        private void ExportAttributes(XmlElement element, CodeClass codeClass)
        {
            var attributeNames = element
                                 .Attributes
                                 .Cast<XmlAttribute>()
                                 .Select(v => v.Name)
                                 .ToArray();
            codeClass.AddAttributes(attributeNames);
        }

        private class CodeClass
        {
            public CodeClass(string name)
            {
                Name = name;
                Attributes = new List<string>();
                Elements = new List<string>();
                ArrayElements = new List<string>();
            }

            public string Name { get; }
            public ICollection<string> Attributes { get; }
            public ICollection<string> Elements { get; }
            public ICollection<string> ArrayElements { get; }

            public void AddAttributes(IEnumerable<string> attributeNames)
            {
                attributeNames
                    .Where(name => !Attributes.Contains(name))
                    .ForEach(name => Attributes.Add(name));
            }

            public void AddArrayElements(IEnumerable<string> elementNames)
            {
                elementNames
                    .ForEach(
                        name =>
                        {
                            Elements.Remove(name);
                            if (!ArrayElements.Contains(name))
                            {
                                ArrayElements.Add(name);
                            }
                        }
                    );
            }

            public void AddElements(IEnumerable<string> elementNames)
            {
                elementNames
                    .ForEach(
                        name =>
                        {
                            if (ArrayElements.Contains(name))
                            {
                                return;
                            }

                            if (!Elements.Contains(name))
                            {
                                Elements.Add(name);
                            }
                        }
                    );
            }
        }
    }
}
