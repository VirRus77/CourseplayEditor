using System.ComponentModel;
using System.Xml.Serialization;

namespace CourseplayEditor.Tools.Courseplay.v2019
{
    [XmlRoot("course")]
    public class Course
    {
        public static readonly XmlSerializer Serializer = new XmlSerializer(
            typeof(Course),
            new[] { typeof(Waypoint) }
        );

        [XmlIgnore]
        public string Name { get; set; }

        [XmlAttribute("workWidth")]
        [DefaultValue(0f)]
        public float WorkWidth { get; set; }

        [XmlAttribute("numHeadlandLanes")]
        [DefaultValue(0)]
        public int NumHeadlandLanes { get; set; }

        [XmlAttribute("headlandDirectionCW")]
        [DefaultValue(false)]
        public bool HeadlandDirectionCW { get; set; }

        [XmlElement("waypoint")]
        public Waypoint[] Waypoints { get; set; } = new Waypoint[0];
    }
}
