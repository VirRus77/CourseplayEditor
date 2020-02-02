using System.Xml.Serialization;

namespace CourseplayEditor.Tools.FarmSimulator.v2019.Map
{
    public class TransformGroup
    {
        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlAttribute("visibility")]
        public string Visibility { get; set; }

        [XmlAttribute("nodeId")]
        public uint NodeId { get; set; }

        [XmlAttribute("translation")]
        public string Translation { get; set; }

        [XmlAttribute("rotation")]
        public string Rotation { get; set; }

        [XmlAttribute("clipDistance")]
        public string ClipDistance { get; set; }

        [XmlAttribute("lodDistance")]
        public string LodDistance { get; set; }

        [XmlAttribute("scale")]
        public string Scale { get; set; }

        [XmlAttribute("objectMask")]
        public string ObjectMask { get; set; }

        [XmlAttribute("static")]
        public string Static { get; set; }

        [XmlAttribute("joint")]
        public string Joint { get; set; }

        [XmlAttribute("breakableJoint")]
        public string BreakableJoint { get; set; }

        [XmlAttribute("jointBreakForce")]
        public string JointBreakForce { get; set; }

        [XmlAttribute("jointBreakTorque")]
        public string JointBreakTorque { get; set; }

        [XmlAttribute("rotMinX")]
        public string RotMinX { get; set; }

        [XmlAttribute("rotMaxX")]
        public string RotMaxX { get; set; }

        [XmlAttribute("transMinX")]
        public string TransMinX { get; set; }

        [XmlAttribute("transMaxX")]
        public string TransMaxX { get; set; }

        [XmlAttribute("rotMinY")]
        public string RotMinY { get; set; }

        [XmlAttribute("rotMaxY")]
        public string RotMaxY { get; set; }

        [XmlAttribute("transMinY")]
        public string TransMinY { get; set; }

        [XmlAttribute("transMaxY")]
        public string TransMaxY { get; set; }

        [XmlAttribute("rotMinZ")]
        public string RotMinZ { get; set; }

        [XmlAttribute("rotMaxZ")]
        public string RotMaxZ { get; set; }

        [XmlAttribute("transMinZ")]
        public string TransMinZ { get; set; }

        [XmlAttribute("transMaxZ")]
        public string TransMaxZ { get; set; }

        [XmlElement("Camera")]
        public Camera Camera { get; set; }

        [XmlElement("Shape")]
        public Shape[] Shapes { get; set; }

        [XmlElement("TransformGroup")]
        public TransformGroup[] TransformGroups { get; set; }

        [XmlElement("Light")]
        public Light[] Light { get; set; }

        [XmlElement("Dynamic")]
        public Dynamic[] Dynamic { get; set; }
    }
}
