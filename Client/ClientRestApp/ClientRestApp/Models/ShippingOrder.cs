using System.Xml.Serialization;

namespace ClientRestApp.Models
{
    [XmlRoot("ShippingOrder")]
    public class ShippingOrder
    {
        public ShippingOrder()
        {
        }

        public ShippingOrder(Address sourceAddress, Address targetAddress, PackageDimension pkgDimension)
        {
            this.SourceAddress    = sourceAddress;
            this.TargetAddress    = targetAddress;
            this.PackageDimension = pkgDimension;
        }

        [XmlElement("SourceAddress")]
        public Address SourceAddress { get; set; }

        [XmlElement("TargetAddress")]
        public Address TargetAddress { get; set; }

        [XmlElement("PkgDimension")]
        public PackageDimension PackageDimension { get; set; }

    }
}