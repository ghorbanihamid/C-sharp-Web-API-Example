
using System;
using System.Xml.Serialization;

namespace ClientRestApp.Models
{

    [Serializable]    
    public class Address
    {
        public Address()
        {
        }

        public Address(int unitNumber, string streetName, string cityName, string postalCode)
        {
            this.UnitNumber = unitNumber;
            this.StreetName = streetName;
            this.CityName   = cityName;
            this.PostalCode = postalCode;
        }

        [XmlElement]
        public int UnitNumber { get; set; }

        [XmlElement]
        public string StreetName { get; set; }

        [XmlElement]
        public string CityName { get; set; }

        [XmlElement]
        public string PostalCode { get; set; }


        
    }
}