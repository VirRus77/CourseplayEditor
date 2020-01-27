using System.Xml.Serialization;

namespace CourseplayEditor.Tools.Courseplay.v2019
{
    public class SaveSlot
    {
        [XmlAttribute("id")]
        public int Id { get; set; }

        [XmlAttribute("fileName")]
        public string FileName { get; set; }

        [XmlAttribute("parent")]
        public int ParenId { get; set; }

        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlAttribute("isUsed")]
        public bool IsUsed { get; set; }
    }
}
