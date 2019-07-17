using FreightApp.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestApp.Helper;
using System;
using System.Collections;
using System.Net.Http;
using System.Threading.Tasks;

namespace FreightApp
{
    class Program
    {
        private static ArrayList companyList = new ArrayList();
        private static CompanyInfo selectedCompany = null;

        static void Main(string[] args)
        {

            try
            {
                // this method takes information from user and converts it to appropriate format for calling api
                ShippingOrder shippingData = GetShippingInfoFromUser();

                // this methods gets companies info from DB and call their api's 
                GetCompaniesInfoFromDB(); 

                foreach (CompanyInfo companyInfo in companyList)
                {
                    // first we must convert the input data to API input format
                    string convertedShippingData = ConvertShippingDataToAPIFormat(companyInfo.InputFormat, shippingData);

                    // if convertedShippingData is empty it means we problem with converting data, so no need to call api, just log the error for trouble shooting 
                    if (convertedShippingData == string.Empty)
                    {
                        Console.WriteLine($" Error ConvertShippingDataToAPIFormat {companyInfo.InputFormat}");
                        continue;
                    }
                    CallCompaniesWebApi(companyInfo, convertedShippingData).Wait();
                }

                Console.WriteLine($"The best Price belong to {selectedCompany.CompanyName} , the price is {selectedCompany.Price}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Freight App exception: {ex.ToString()}");
            }

        }

        static async Task CallCompaniesWebApi(CompanyInfo companyInfo, string shippingData)
        {
            try
            {
                using (HttpClient httpClient = new HttpClient())
                {
                    HttpResponseMessage response = await httpClient.PostAsJsonAsync(companyInfo.RestApiUrl, shippingData);
                    {
                        response.EnsureSuccessStatusCode();
                        using (HttpContent content = response.Content)
                        {
                            string apiResult = await content.ReadAsStringAsync();

                            // Extracting Price from RestAPI result
                            companyInfo.Price = ExtractPriceFromAPIResult(companyInfo.OutputFormat, apiResult);

                            // compare the result for selecting minimum price
                            // selectedCompany == null means this is the first company we called it's API
                            if (selectedCompany == null || selectedCompany.Price > companyInfo.Price) 
                            {
                                selectedCompany = companyInfo;
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"FreightApp.CallRestApi() exception: {e.ToString()}");
            }
        }

        static void GetCompaniesInfoFromDB()
        {

            // suppose companies information is fetched from Database

            // Add First Company Info
            CompanyInfo companyInfo = new CompanyInfo(1,"x", "http://localhost:51222/api/firstcompany", "json","text");
            companyList.Add(companyInfo);

            // Add Second Company Info
            companyInfo = new CompanyInfo(1, "y", "http://localhost:51222/api/secondcompany", "json", "text");
            companyList.Add(companyInfo);

            // Add third Company Info
            companyInfo = new CompanyInfo(1, "z", "http://localhost:51222/api/thirdcompany", "xml", "xml");
            companyList.Add(companyInfo);
            
        }

        private static ShippingOrder GetShippingInfoFromUser()
        {

            // we need to take the shipping info including source address, destination address 
            // and package dimension from user and convert it to appropriate json or xml

            ShippingOrder shippingOrder = new ShippingOrder();

            shippingOrder.SourceAddress = new Address(100,"Marine Dr","Vancouver","V3C YPZ");

            shippingOrder.TargetAddress = new Address(200, "Marine Dr", "Toronto", "C4C YPZ");

            shippingOrder.PkgDimension = new PackageDimension(20,50,30);

            return shippingOrder;
        }

        private static string ConvertShippingDataToAPIFormat(String format,ShippingOrder shippingOrder)
        {
            try
            {
                // we need to convert given data to appropriate type for APIs

                switch (format)
                {
                    case "JSON":
                        return JsonConvert.SerializeObject(shippingOrder);

                    case "XML":
                        return XmlHelper.GetXMLFromObject(shippingOrder);

                    default: return "";
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"FreightApp.ConvertShippingDataToAPIFormat() exception: {e.ToString()}");
                return "";
            }
            
        }

        private static float ExtractPriceFromAPIResult(string outputFormat, string apiResult)
        {
            float result = 0;
            // extract price from apiResult accord to output type
            switch (outputFormat)
            {
                case "text":
                    result = float.Parse(apiResult);// just convert the string to float
                    break;
                case "json":
                    // parse the json and get the value
                    JObject.Parse(apiResult);
                    break;
                case "xml":
                    // parse the xml and get the value
                    break;

            }
            return result;
        }
    }
}
