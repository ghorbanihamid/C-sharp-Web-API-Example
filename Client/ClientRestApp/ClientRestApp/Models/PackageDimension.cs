
using System;
using System.Xml.Serialization;

namespace ClientRestApp.Models
{
    [Serializable]
    public class PackageDimension
    {
        public PackageDimension()
        {
        }

        public PackageDimension(float length, float width, float height)
        {
            this.Length = length;
            this.Width = width;
            this.Height = height;
        }

        [XmlElement]
        public float Length { get; set; }

        [XmlElement]
        public float Width { get; set; }

        [XmlElement]
        public float Height { get; set; }
    }

    

}