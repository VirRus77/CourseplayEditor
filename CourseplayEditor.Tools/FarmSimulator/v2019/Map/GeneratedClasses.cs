using System.Xml.Serialization;

namespace CourseplayEditor.Tools.FarmSimulator.v2019.Map
{
    public class Files
    {
        [XmlElement("File")]
        public File[] File { get; set; }
    }

    public class Materials
    {
        [XmlElement("Material")]
        public Material[] Material { get; set; }
    }

    public class Material
    {
        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlAttribute("materialId")]
        public string MaterialId { get; set; }

        [XmlAttribute("diffuseColor")]
        public string DiffuseColor { get; set; }

        [XmlAttribute("specularColor")]
        public string SpecularColor { get; set; }

        [XmlAttribute("customShaderId")]
        public string CustomShaderId { get; set; }

        [XmlAttribute("customShaderVariation")]
        public string CustomShaderVariation { get; set; }

        [XmlAttribute("alphaBlending")]
        public string AlphaBlending { get; set; }

        [XmlElement("Normalmap")]
        public Normalmap Normalmap { get; set; }

        [XmlElement("Texture")]
        public Texture Texture { get; set; }

        [XmlElement("Glossmap")]
        public Glossmap Glossmap { get; set; }

        [XmlElement("Refractionmap")]
        public Refractionmap Refractionmap { get; set; }

        [XmlElement("Emissivemap")]
        public Emissivemap Emissivemap { get; set; }

        [XmlElement("Reflectionmap")]
        public Reflectionmap Reflectionmap { get; set; }

        [XmlElement("CustomParameter")]
        public CustomParameter[] CustomParameter { get; set; }

        [XmlElement("Custommap")]
        public Custommap[] Custommap { get; set; }
    }

    public class Normalmap
    {
        [XmlAttribute("fileId")]
        public string FileId { get; set; }

        [XmlAttribute("bumpDepth")]
        public string BumpDepth { get; set; }
    }

    public class CustomParameter
    {
        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlAttribute("value")]
        public string Value { get; set; }
    }

    public class Texture
    {
        [XmlAttribute("fileId")]
        public string FileId { get; set; }
    }

    public class Glossmap
    {
        [XmlAttribute("fileId")]
        public string FileId { get; set; }
    }

    public class Custommap
    {
        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlAttribute("fileId")]
        public string FileId { get; set; }
    }

    public class Refractionmap
    {
        [XmlAttribute("coeff")]
        public string Coeff { get; set; }

        [XmlAttribute("bumpScale")]
        public string BumpScale { get; set; }
    }

    public class Emissivemap
    {
        [XmlAttribute("fileId")]
        public string FileId { get; set; }
    }

    public class Reflectionmap
    {
        [XmlAttribute("lodDistanceScaling")]
        public string LodDistanceScaling { get; set; }

        [XmlAttribute("viewDistanceScaling")]
        public string ViewDistanceScaling { get; set; }

        [XmlAttribute("type")]
        public string Type { get; set; }

        [XmlAttribute("scaling")]
        public string Scaling { get; set; }

        [XmlAttribute("refractiveIndex")]
        public string RefractiveIndex { get; set; }

        [XmlAttribute("bumpScale")]
        public string BumpScale { get; set; }

        [XmlAttribute("shapesObjectMask")]
        public string ShapesObjectMask { get; set; }

        [XmlAttribute("lightsObjectMask")]
        public string LightsObjectMask { get; set; }
    }

    public class Shapes
    {
        [XmlAttribute("externalShapesFile")]
        public string ExternalShapesFile { get; set; }
    }

    public class Dynamics
    {
        [XmlElement("ParticleSystem")]
        public ParticleSystem[] ParticleSystem { get; set; }
    }

    public class ParticleSystem
    {
        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlAttribute("dynamicId")]
        public string DynamicId { get; set; }

        [XmlAttribute("type")]
        public string Type { get; set; }

        [XmlAttribute("rate")]
        public string Rate { get; set; }

        [XmlAttribute("lifespan")]
        public string Lifespan { get; set; }

        [XmlAttribute("maxCount")]
        public string MaxCount { get; set; }

        [XmlAttribute("speed")]
        public string Speed { get; set; }

        [XmlAttribute("speedRandom")]
        public string SpeedRandom { get; set; }

        [XmlAttribute("tangentSpeed")]
        public string TangentSpeed { get; set; }

        [XmlAttribute("normalSpeed")]
        public string NormalSpeed { get; set; }

        [XmlAttribute("spriteScaleX")]
        public string SpriteScaleX { get; set; }

        [XmlAttribute("spriteScaleY")]
        public string SpriteScaleY { get; set; }

        [XmlAttribute("spriteScaleXGain")]
        public string SpriteScaleXGain { get; set; }

        [XmlAttribute("spriteScaleYGain")]
        public string SpriteScaleYGain { get; set; }

        [XmlAttribute("blendFactor")]
        public string BlendFactor { get; set; }

        [XmlAttribute("blendInFactor")]
        public string BlendInFactor { get; set; }

        [XmlAttribute("blendOutFactor")]
        public string BlendOutFactor { get; set; }

        [XmlAttribute("randomInitRotation")]
        public string RandomInitRotation { get; set; }

        [XmlAttribute("deltaRotateMax")]
        public string DeltaRotateMax { get; set; }

        [XmlAttribute("textureAtlasSizeX")]
        public string TextureAtlasSizeX { get; set; }

        [XmlElement("Gravity")]
        public Gravity Gravity { get; set; }
    }

    public class Gravity
    {
        [XmlAttribute("force")]
        public string Force { get; set; }
    }

    public class Scene
    {
        [XmlElement("Camera")]
        public Camera Camera { get; set; }

        [XmlElement("Light")]
        public Light Light { get; set; }

        [XmlElement("TerrainTransformGroup")]
        public TerrainTransformGroup TerrainTransformGroup { get; set; }

        [XmlElement("TransformGroup")]
        public TransformGroup[] TransformGroup { get; set; }
    }

    public class Camera
    {
        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlAttribute("translation")]
        public string Translation { get; set; }

        [XmlAttribute("rotation")]
        public string Rotation { get; set; }

        [XmlAttribute("visibility")]
        public string Visibility { get; set; }

        [XmlAttribute("nodeId")]
        public string NodeId { get; set; }

        [XmlAttribute("fov")]
        public string Fov { get; set; }

        [XmlAttribute("nearClip")]
        public string NearClip { get; set; }

        [XmlAttribute("farClip")]
        public string FarClip { get; set; }

        [XmlAttribute("orthographicHeight")]
        public string OrthographicHeight { get; set; }
    }

    public class Light
    {
        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlAttribute("translation")]
        public string Translation { get; set; }

        [XmlAttribute("rotation")]
        public string Rotation { get; set; }

        [XmlAttribute("objectMask")]
        public string ObjectMask { get; set; }

        [XmlAttribute("nodeId")]
        public string NodeId { get; set; }

        [XmlAttribute("type")]
        public string Type { get; set; }

        [XmlAttribute("color")]
        public string Color { get; set; }

        [XmlAttribute("emitDiffuse")]
        public string EmitDiffuse { get; set; }

        [XmlAttribute("emitSpecular")]
        public string EmitSpecular { get; set; }

        [XmlAttribute("castShadowMap")]
        public string CastShadowMap { get; set; }

        [XmlAttribute("depthMapBias")]
        public string DepthMapBias { get; set; }

        [XmlAttribute("depthMapSlopeScaleBias")]
        public string DepthMapSlopeScaleBias { get; set; }

        [XmlAttribute("depthMapSlopeClamp")]
        public string DepthMapSlopeClamp { get; set; }

        [XmlAttribute("depthMapResolution")]
        public string DepthMapResolution { get; set; }

        [XmlAttribute("shadowFarDistance")]
        public string ShadowFarDistance { get; set; }

        [XmlAttribute("shadowExtrusionDistance")]
        public string ShadowExtrusionDistance { get; set; }

        [XmlAttribute("shadowPerspective")]
        public string ShadowPerspective { get; set; }

        [XmlAttribute("numShadowMapSplits")]
        public string NumShadowMapSplits { get; set; }

        [XmlAttribute("shadowMapSplitDistance0")]
        public string ShadowMapSplitDistance0 { get; set; }

        [XmlAttribute("shadowMapSplitDistance1")]
        public string ShadowMapSplitDistance1 { get; set; }

        [XmlAttribute("shadowMapSplitDistance2")]
        public string ShadowMapSplitDistance2 { get; set; }

        [XmlAttribute("shadowMapSplitDistance3")]
        public string ShadowMapSplitDistance3 { get; set; }

        [XmlAttribute("decayRate")]
        public string DecayRate { get; set; }

        [XmlAttribute("range")]
        public string Range { get; set; }

        [XmlAttribute("scattering")]
        public string Scattering { get; set; }

        [XmlAttribute("coneAngle")]
        public string ConeAngle { get; set; }

        [XmlAttribute("dropOff")]
        public string DropOff { get; set; }

        [XmlAttribute("visibility")]
        public string Visibility { get; set; }

        [XmlAttribute("clipDistance")]
        public string ClipDistance { get; set; }

        [XmlAttribute("scale")]
        public string Scale { get; set; }
    }

    public class Layers
    {
        [XmlElement("LayerCombiner")]
        public LayerCombiner LayerCombiner { get; set; }

        [XmlElement("Layer")]
        public Layer[] Layer { get; set; }

        [XmlElement("CombinedLayer")]
        public CombinedLayer[] CombinedLayer { get; set; }

        [XmlElement("InfoLayer")]
        public InfoLayer[] InfoLayer { get; set; }

        [XmlElement("DetailLayer")]
        public DetailLayer[] DetailLayer { get; set; }

        [XmlElement("FoliageMultiLayer")]
        public FoliageMultiLayer[] FoliageMultiLayer { get; set; }
    }

    public class Layer
    {
        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlAttribute("detailMapId")]
        public string DetailMapId { get; set; }

        [XmlAttribute("normalMapId")]
        public string NormalMapId { get; set; }

        [XmlAttribute("unitSize")]
        public string UnitSize { get; set; }

        [XmlAttribute("weightMapId")]
        public string WeightMapId { get; set; }

        [XmlAttribute("blendContrast")]
        public string BlendContrast { get; set; }

        [XmlAttribute("distanceMapId")]
        public string DistanceMapId { get; set; }

        [XmlAttribute("attributes")]
        public string Attributes { get; set; }

        [XmlAttribute("priority")]
        public string Priority { get; set; }
    }

    public class CombinedLayer
    {
        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlAttribute("layers")]
        public string Layers { get; set; }

        [XmlAttribute("noiseFrequency")]
        public string NoiseFrequency { get; set; }
    }

    public class LayerCombiner
    {
        [XmlAttribute("defaultDepthScale")]
        public string DefaultDepthScale { get; set; }

        [XmlAttribute("defaultSharpness")]
        public string DefaultSharpness { get; set; }
    }

    public class InfoLayer
    {
        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlAttribute("fileId")]
        public string FileId { get; set; }

        [XmlAttribute("numChannels")]
        public string NumChannels { get; set; }

        [XmlAttribute("runtime")]
        public string Runtime { get; set; }
    }

    public class DetailLayer
    {
        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlAttribute("densityMapId")]
        public string DensityMapId { get; set; }

        [XmlAttribute("numDensityMapChannels")]
        public string NumDensityMapChannels { get; set; }

        [XmlAttribute("compressionChannels")]
        public string CompressionChannels { get; set; }

        [XmlAttribute("cellSize")]
        public string CellSize { get; set; }

        [XmlAttribute("objectMask")]
        public string ObjectMask { get; set; }

        [XmlAttribute("decalLayer")]
        public string DecalLayer { get; set; }

        [XmlAttribute("distanceMapIds")]
        public string DistanceMapIds { get; set; }

        [XmlAttribute("distanceMapFirstChannel")]
        public string DistanceMapFirstChannel { get; set; }

        [XmlAttribute("distanceMapNumChannels")]
        public string DistanceMapNumChannels { get; set; }

        [XmlAttribute("materialId")]
        public string MaterialId { get; set; }

        [XmlAttribute("viewDistance")]
        public string ViewDistance { get; set; }

        [XmlAttribute("blendOutDistance")]
        public string BlendOutDistance { get; set; }

        [XmlAttribute("densityMapShaderNames")]
        public string DensityMapShaderNames { get; set; }

        [XmlAttribute("combinedValuesChannels")]
        public string CombinedValuesChannels { get; set; }

        [XmlAttribute("useInterpolatedDensityMap")]
        public string UseInterpolatedDensityMap { get; set; }

        [XmlAttribute("heightFirstChannel")]
        public string HeightFirstChannel { get; set; }

        [XmlAttribute("heightNumChannels")]
        public string HeightNumChannels { get; set; }

        [XmlAttribute("maxHeight")]
        public string MaxHeight { get; set; }
    }

    public class FoliageMultiLayer
    {
        [XmlAttribute("densityMapId")]
        public string DensityMapId { get; set; }

        [XmlAttribute("numChannels")]
        public string NumChannels { get; set; }

        [XmlAttribute("numTypeIndexChannels")]
        public string NumTypeIndexChannels { get; set; }

        [XmlAttribute("compressionChannels")]
        public string CompressionChannels { get; set; }

        [XmlElement("FoliageType")]
        public FoliageType[] FoliageType { get; set; }
    }

    public class FoliageType
    {
        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlAttribute("foliageXmlId")]
        public string FoliageXmlId { get; set; }
    }

    public class Dynamic
    {
        [XmlAttribute("dynamicId")]
        public string DynamicId { get; set; }

        [XmlAttribute("emitterShapeNodeId")]
        public string EmitterShapeNodeId { get; set; }

        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlAttribute("translation")]
        public string Translation { get; set; }

        [XmlAttribute("clipDistance")]
        public string ClipDistance { get; set; }

        [XmlAttribute("objectMask")]
        public string ObjectMask { get; set; }

        [XmlAttribute("nodeId")]
        public string NodeId { get; set; }

        [XmlAttribute("materialIds")]
        public string MaterialIds { get; set; }

        [XmlAttribute("distanceBlending")]
        public string DistanceBlending { get; set; }

        [XmlAttribute("rotation")]
        public string Rotation { get; set; }

        [XmlAttribute("scale")]
        public string Scale { get; set; }

        [XmlElement("Shape")]
        public Shape Shape { get; set; }
    }

    public class UserAttributes
    {
        [XmlElement("UserAttribute")]
        public UserAttribute[] UserAttribute { get; set; }
    }

    public class UserAttribute
    {
        [XmlAttribute("nodeId")]
        public string NodeId { get; set; }

        [XmlElement("Attribute")]
        public Attribute[] Attribute { get; set; }
    }

    public class Attribute
    {
        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlAttribute("type")]
        public string Type { get; set; }

        [XmlAttribute("value")]
        public string Value { get; set; }
    }
}
