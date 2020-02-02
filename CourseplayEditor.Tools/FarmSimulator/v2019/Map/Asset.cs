using System.Xml.Serialization;

namespace CourseplayEditor.Tools.FarmSimulator.v2019.Map
{
    public class Asset
    {

        [XmlElement("Export")]
        public Export[] Exports { get; set; }
    }
}
