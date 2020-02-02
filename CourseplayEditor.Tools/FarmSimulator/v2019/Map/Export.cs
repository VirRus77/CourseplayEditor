using System.Xml.Serialization;

namespace CourseplayEditor.Tools.FarmSimulator.v2019.Map
{
    public class Export
    {
        [XmlAttribute("program")]
        public string Program { get; set; }

        [XmlAttribute("version")]
        public string Version { get; set; }
    }
}
