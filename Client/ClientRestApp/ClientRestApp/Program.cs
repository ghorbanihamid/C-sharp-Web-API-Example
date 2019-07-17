using ClientRestApp.Helper;
using ClientRestApp.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace ClientRestApp
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

                string xmlStr = XmlHelper.GetXMLFromObject(shippingData);
                ShippingOrder shippingOrder = (ShippingOrder)XmlHelper.GetObjectFromXml(xmlStr, typeof(ShippingOrder));


                // this methods gets companies info from DB and call their api's 
                GetCompaniesInfoFromDB();

                int Counter = 1;
                foreach (CompanyInfo companyInfo in companyList)
                {
                    Console.WriteLine(Counter + ")");
                    // first we must convert the input data to API input format
                    string convertedShippingData = ConvertShippingDataToAPIFormat(companyInfo.InputFormat, shippingData);

                    // if convertedShippingData is empty it means we problem with converting data, so no need to call api, just log the error for trouble shooting 
                    if (convertedShippingData == string.Empty)
                    {
                        Console.WriteLine($" Error ConvertShippingDataToAPIFormat {companyInfo.InputFormat}");
                        continue;
                    }
                    CallCompaniesWebApi(companyInfo, convertedShippingData).Wait();
                    Counter++;
                }

                Console.WriteLine("Result : ");
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
                    var stringContent = new StringContent(shippingData, System.Text.Encoding.UTF8, "application/json");                                                      
                    HttpResponseMessage response = await httpClient.PostAsync(companyInfo.RestApiUrl, stringContent);
                    {
                        //response.EnsureSuccessStatusCode();
                        using (HttpContent content = response.Content)
                        {
                            string apiResult = await content.ReadAsStringAsync();

                            Console.WriteLine("Web Service of '" + companyInfo.CompanyName + "' returned result is : " + apiResult);

                            // Extracting Price from RestAPI result
                            companyInfo.Price = ExtractPriceFromAPIResult(companyInfo.OutputFormat, apiResult);

                            Console.WriteLine("The suggested price of '" + companyInfo.CompanyName + "' is : " + companyInfo.Price + " CAD");

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
            CompanyInfo companyInfo = new CompanyInfo(1, "Global Freight Co. ", "http://localhost:51222/api/FirstCompany/CalculatePrice", "json", "json");
            companyList.Add(companyInfo);

            // Add Second Company Info
            companyInfo = new CompanyInfo(1, "Vancouver Ships Co. ", "http://localhost:51222/api/SecondCompany/AskShippingPrice", "json", "text");
            companyList.Add(companyInfo);

            // Add third Company Info
            //companyInfo = new CompanyInfo(1, "Amexican Express Co. ", "http://localhost:51222/api/thirdcompany", "xml", "xml");
            //companyList.Add(companyInfo);

        }

        private static ShippingOrder GetShippingInfoFromUser()
        {

            // we need to take the shipping info including source address, destination address 
            // and package dimension from user and convert it to appropriate json or xml

            ShippingOrder shippingOrder = new ShippingOrder();

            shippingOrder.SourceAddress = new Address(100, "Marine Dr", "Vancouver", "V3C YPZ");

            shippingOrder.TargetAddress = new Address(200, "Marine Dr", "Toronto", "C4C YPZ");

            shippingOrder.PackageDimension = new PackageDimension(20, 50, 30);

            return shippingOrder;
        }

        private static string ConvertShippingDataToAPIFormat(String format, ShippingOrder shippingOrder)
        {
            try
            {
                // we need to convert given data to appropriate type for APIs

                switch (format.ToUpper())
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
            switch (outputFormat.ToUpper())
            {
                case "TEXT":
                    result = float.Parse(apiResult);// just convert the string to float
                    break;
                case "JSON":
                    // parse the json and get the value
                    Object obj = JObject.Parse(apiResult).First;
                    string stringResult = ((JValue)((JProperty)obj).Value).Value.ToString();
                    result = float.Parse(stringResult);
                    break;
                case "XML":
                    // parse the xml and get the value
                    break;

            }
            return result;
        }
    }
}
