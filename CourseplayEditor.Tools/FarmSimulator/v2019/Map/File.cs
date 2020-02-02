using System;
using System.Xml.Serialization;

namespace CourseplayEditor.Tools.FarmSimulator.v2019.Map
{
    public class File
    {
        [XmlAttribute("fileId")]
        public uint FileId { get; set; }

        [XmlAttribute("filename")]
        public string Filename { get; set; }
    }
}
