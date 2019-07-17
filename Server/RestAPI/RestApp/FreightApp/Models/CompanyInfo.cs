
namespace FreightApp.Models
{
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

        public int CompanyId { get; set; }
        public string CompanyName { get; set; }    
        public string RestApiUrl { get; set; }    // address of RestApi of company  
        public string InputFormat { get; set; }   // format of data for sending to api, example : text, json, xml
        public string OutputFormat { get; set; }  // format of output data returning from api, example : text, json, xml
        public float Price { get; set; }
    }
}
