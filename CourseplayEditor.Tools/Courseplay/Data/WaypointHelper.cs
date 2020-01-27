using System.IO;
using System.Xml;
using System.Xml.Xsl;
using CourseplayEditor.Tools.Tools;
using CourseplayEditor.Tools.Tools.Extensions;

namespace CourseplayEditor.Tools.Courseplay.Data
{
    public static class WaypointHelper
    {
        private const string FromCourse = "FromCourse.xslt";
        private const string ToCourse = "ToCourse.xslt";

        private static readonly XslCompiledTransform TransformFromCourse = new XslCompiledTransform();
        private static readonly XslCompiledTransform TransformToCourse = new XslCompiledTransform();

        static WaypointHelper()
        {
            LoadTransform(FromCourse, TransformFromCourse);
            LoadTransform(ToCourse, TransformToCourse);
        }

        public static Stream FromCoursePlay(XmlDocument xml)
        {
            return TransformFromCourse.TransformToXmlStream(xml);
        }

        public static XmlDocument ToCoursePlay(XmlDocument xml)
        {
            return TransformToCourse.TransformToXmlDocument(xml);
        }

        private static void LoadTransform(string fileName, XslCompiledTransform xslCompiledTransform)
        {
            using (var stream = EmbeddedResources.ReadResource(typeof(WaypointHelper).Assembly, fileName))
            {
                var xml = new XmlDocument();
                xml.Load(stream);
                xslCompiledTransform.Load(xml);
            }
        }
    }
}
