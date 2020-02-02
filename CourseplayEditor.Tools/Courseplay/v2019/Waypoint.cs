using System.ComponentModel;
using System.Xml.Serialization;

namespace CourseplayEditor.Tools.Courseplay.v2019
{
    public class Waypoint
    {
        [XmlAttribute("speed")]
        public int Speed { get; set; }

        [XmlAttribute("angle")]
        public float Angle { get; set; }

        [XmlAttribute("pos")]
        public string Position { get; set; }

        [XmlAttribute("pointX")]
        public float PointX { get; set; }

        [XmlAttribute("pointY")]
        public float PointY { get; set; }

        [XmlAttribute("pointZ")]
        public float PointZ { get; set; }

        [XmlAttribute("rev")]
        [DefaultValue(0)]
        public int Reverse { get; set; }

        [XmlAttribute("crossing")]
        public int Crossing { get; set; }

        [XmlAttribute("turnstart")]
        [DefaultValue(0)]
        public int TurnStart { get; set; }

        [XmlAttribute("turnend")]
        [DefaultValue(0)]
        public int TurnEnd { get; set; }

        [XmlAttribute("wait")]
        [DefaultValue(0)]
        public int Wait { get; set; }

        [XmlAttribute("unload")]
        [DefaultValue(0)]
        public int Unload { get; set; }

        [XmlAttribute("generated")]
        [DefaultValue(false)]
        public bool Generated { get; set; }

        [XmlAttribute("ridgemarker")]
        [DefaultValue(0)]
        public int Ridgemarker { get; set; }
    }
}
