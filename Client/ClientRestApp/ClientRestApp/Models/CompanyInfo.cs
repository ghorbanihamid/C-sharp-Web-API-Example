
using System.Xml.Serialization;

namespace ClientRestApp.Models
{
    [XmlRoot("CompanyInfo")]
    class CompanyInfo
    {
        public CompanyInfo()
        {
        }

        public CompanyInfo(int companyId, string companyName, string restApiUrl, string inputFormat, string outputFormat)
        {
            CompanyId = companyId;
            CompanyName = companyName;
            RestApiUrl = restApiUrl;
            InputFormat = inputFormat;
            OutputFormat = outputFormat;
        }

        [XmlElement]
        public int CompanyId { get; set; }

        [XmlElement]
        public string CompanyName { get; set; }

        [XmlElement]
        public string RestApiUrl { get; set; }    // address of RestApi of company  

        [XmlElement]
        public string InputFormat { get; set; }   // format of data for sending to api, example : text, json, xml

        [XmlElement]
        public string OutputFormat { get; set; }  // format of output data returning from api, example : text, json, xml

        [XmlElement]
        public float Price { get; set; }
    }
}
