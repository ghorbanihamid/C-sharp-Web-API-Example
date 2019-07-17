using Newtonsoft.Json;
using System;
using System.Web.Http;
using RestApp.Views;
using Newtonsoft.Json.Linq;

namespace RestApp.Controllers
{
    // This Controler accepts json as input data
    [Route("api/FirstCompany")]
    public class FirstCompanyController : ApiController
    {
        // GET api/FirstCompany
        public string Get()
        {
            return "This is First Company API";
        }

        // GET api/FirstCompany/5
        public string Get(int id)
        {
            return "First Company API, entered Param : " + id;
        }

        // POST "api/FirstCompany/CalculatePrice
        [HttpPost, Route("api/FirstCompany/CalculatePrice")]
        public IHttpActionResult CalculatePrice([FromBody]ShippingOrder inputData)
        {

            string outPutResult = string.Empty;
            try
            {                
                if (inputData == null)
                {
                    return BadRequest("FirstCompany api, Invalid request, no data");
                }
                // Calculate the shipping price according to input data
                float price = CalculatePriceFromInput(inputData);    

                outPutResult =@"{'total':" + price + "}";
               
                //Object result1 = JObject.Parse(outPutResult);
                Object result2 = JsonConvert.DeserializeObject(outPutResult);
                return Ok(result2);


            }
            catch (Exception e)
            {
                return BadRequest("FirstCompany api, Error in web service :" + e.Message);
            }
            
        }

        private float CalculatePriceFromInput(ShippingOrder shippingOrder)
        {
            // calculate the price according to given data 
            return 20;
        }

    }
}
