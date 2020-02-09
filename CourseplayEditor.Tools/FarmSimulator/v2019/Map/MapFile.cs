using System.IO;
using System.Xml.Serialization;

namespace CourseplayEditor.Tools.FarmSimulator.v2019.Map
{
    /// <summary>
    /// Open xml map file *.i3d
    /// </summary>
    [XmlRoot("i3D")]
    public class MapFile
    {
        protected MapFile()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        public static readonly XmlSerializer Serializer = new XmlSerializer(
            typeof(MapFile),
            new[]
            {
                typeof(Asset),
                typeof(Export),
                typeof(Files),
                typeof(File),
                typeof(Materials),
                typeof(Material),
                typeof(Normalmap),
                typeof(CustomParameter),
                typeof(Texture),
                typeof(Glossmap),
                typeof(Custommap),
                typeof(Refractionmap),
                typeof(Emissivemap),
                typeof(Reflectionmap),
                typeof(Shapes),
                typeof(Dynamics),
                typeof(ParticleSystem),
                typeof(Gravity),
                typeof(Scene),
                typeof(Camera),
                typeof(Light),
                typeof(TerrainTransformGroup),
                typeof(Layers),
                typeof(Layer),
                typeof(CombinedLayer),
                typeof(LayerCombiner),
                typeof(InfoLayer),
                typeof(DetailLayer),
                typeof(FoliageMultiLayer),
                typeof(FoliageType),
                typeof(TransformGroup),
                typeof(Shape),
                typeof(Dynamic),
                typeof(UserAttributes),
                typeof(UserAttribute),
                typeof(Attribute),
            }
        );

        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlAttribute("version")]
        public string Version { get; set; }

        [XmlElement("Asset")]
        public Asset Asset { get; set; }

        [XmlElement("Files")]
        public Files Files { get; set; }

        [XmlElement("Materials")]
        public Materials Materials { get; set; }

        [XmlElement("Shapes")]
        public Shapes Shapes { get; set; }

        [XmlElement("Dynamics")]
        public Dynamics Dynamics { get; set; }

        [XmlElement("Scene")]
        public Scene Scene { get; set; }

        [XmlElement("UserAttributes")]
        public UserAttributes UserAttributes { get; set; }

        public static MapFile Load(string filePath)
        {
            using var stream = System.IO.File.Open(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
            return (MapFile) Serializer.Deserialize(stream);
        }
    }
}
