using System.Xml.Serialization;

namespace CourseplayEditor.Tools.FarmSimulator.v2019.Map
{
    public class TerrainTransformGroup
    {
        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlAttribute("static")]
        public string Static { get; set; }

        [XmlAttribute("collisionMask")]
        public string CollisionMask { get; set; }

        [XmlAttribute("nodeId")]
        public uint NodeId { get; set; }

        [XmlAttribute("heightMapId")]
        public string HeightMapId { get; set; }

        [XmlAttribute("patchSize")]
        public string PatchSize { get; set; }

        [XmlAttribute("maxLODDistance")]
        public string MaxLODDistance { get; set; }

        [XmlAttribute("heightScale")]
        public string HeightScale { get; set; }

        [XmlAttribute("unitsPerPixel")]
        public string UnitsPerPixel { get; set; }

        [XmlAttribute("lodBlendStart")]
        public string LodBlendStart { get; set; }

        [XmlAttribute("lodBlendEnd")]
        public string LodBlendEnd { get; set; }

        [XmlAttribute("lodTextureSize")]
        public string LodTextureSize { get; set; }

        [XmlAttribute("lodBlendStartDynamic")]
        public string LodBlendStartDynamic { get; set; }

        [XmlAttribute("lodBlendEndDynamic")]
        public string LodBlendEndDynamic { get; set; }

        [XmlAttribute("materialId")]
        public uint MaterialId { get; set; }

        [XmlAttribute("castShadowMap")]
        public bool CastShadowMap { get; set; }

        [XmlElement("Layers")]
        public Layers Layers { get; set; }
    }
}
