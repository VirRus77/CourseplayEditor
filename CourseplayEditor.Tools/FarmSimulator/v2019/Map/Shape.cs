using System;
using System.Linq;
using System.Xml.Serialization;

namespace CourseplayEditor.Tools.FarmSimulator.v2019.Map
{
    public class Shape
    {
        [XmlAttribute("shapeId")]
        public uint ShapeId { get; set; }

        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlAttribute("translation")]
        public string Translation { get; set; }

        [XmlAttribute("static")]
        public string Static { get; set; }

        [XmlAttribute("clipDistance")]
        public string ClipDistance { get; set; }

        [XmlAttribute("nodeId")]
        public uint NodeId { get; set; }

        [XmlAttribute("materialIds")]
        public string MaterialIds { get; set; }

        [XmlAttribute("castsShadows")]
        public string CastsShadows { get; set; }

        [XmlAttribute("receiveShadows")]
        public string ReceiveShadows { get; set; }

        [XmlAttribute("nonRenderable")]
        public string NonRenderable { get; set; }

        [XmlAttribute("trigger")]
        public string Trigger { get; set; }

        [XmlAttribute("collisionMask")]
        public string CollisionMask { get; set; }

        [XmlAttribute("rotation")]
        public string Rotation { get; set; }

        [XmlAttribute("decalLayer")]
        public string DecalLayer { get; set; }

        [XmlAttribute("scale")]
        public string Scale { get; set; }

        [XmlAttribute("distanceBlending")]
        public string DistanceBlending { get; set; }

        [XmlAttribute("kinematic")]
        public string Kinematic { get; set; }

        [XmlAttribute("visibility")]
        public string Visibility { get; set; }

        [XmlAttribute("density")]
        public string Density { get; set; }

        [XmlAttribute("dynamic")]
        public string Dynamic { get; set; }

        [XmlAttribute("dynamicFriction")]
        public string DynamicFriction { get; set; }

        [XmlAttribute("restitution")]
        public string Restitution { get; set; }

        [XmlAttribute("staticFriction")]
        public string StaticFriction { get; set; }

        [XmlAttribute("linearDamping")]
        public string LinearDamping { get; set; }

        [XmlAttribute("collision")]
        public string Collision { get; set; }

        [XmlAttribute("objectMask")]
        public string ObjectMask { get; set; }

        [XmlAttribute("angularDamping")]
        public string AngularDamping { get; set; }

        [XmlAttribute("rollingFriction")]
        public string RollingFriction { get; set; }

        [XmlAttribute("splitType")]
        public string SplitType { get; set; }

        [XmlAttribute("splitUvs")]
        public string SplitUvs { get; set; }

        [XmlAttribute("compound")]
        public string Compound { get; set; }

        [XmlAttribute("compoundChild")]
        public string CompoundChild { get; set; }

        [XmlElement("Light")]
        public Light Light { get; set; }

        [XmlElement("Shape")]
        public Shape[] Shapes { get; set; }

        [XmlElement("TransformGroup")]
        public TransformGroup[] TransformGroup { get; set; }

        public float[] TranslationValue
        {
            get
            {
                if (string.IsNullOrEmpty(Translation))
                    return null;
                var values = Translation.Split(new[] { " " }, StringSplitOptions.None);
                if (values.Length != 3)
                {
                    throw new Exception();
                }

                return values.Select(v => float.Parse(v.Replace(".",","))).ToArray();
            }
        }
    }
}
