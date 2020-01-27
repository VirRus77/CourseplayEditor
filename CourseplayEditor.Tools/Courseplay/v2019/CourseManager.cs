using System.Xml.Serialization;

namespace CourseplayEditor.Tools.Courseplay.v2019
{
    [XmlRoot("courseManager")]
    public class CourseManager
    {
        public static readonly XmlSerializer Serializer = new XmlSerializer(
            typeof(CourseManager),
            new[] { typeof(SaveSlot) }
        );

        [XmlArray("saveSlot")]
        [XmlArrayItem("slot")]
        public SaveSlot[] SaveSlots { get; set; } = new SaveSlot[0];
    }
}
