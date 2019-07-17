using System.Xml.Serialization;

namespace FreightApp.Models
{
    [XmlRoot("ShippingOrder")]
    public class ShippingOrder
    {
        public ShippingOrder()
        {
        }

        public ShippingOrder(Address sourceAddress, Address targetAddress, PackageDimension pkgDimension)
        {
            SourceAddress = sourceAddress;
            TargetAddress = targetAddress;
            PkgDimension = pkgDimension;
        }

        [XmlElement("SourceAddress")]
        public Address SourceAddress { get; set; }

        [XmlElement("TargetAddress")]
        public Address TargetAddress { get; set; }

        [XmlElement("PkgDimension")]
        public PackageDimension PkgDimension { get; set; }

    }
}