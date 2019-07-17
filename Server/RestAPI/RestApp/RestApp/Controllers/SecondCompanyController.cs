using Newtonsoft.Json;
using System;
using System.Web.Http;
using RestApp.Views;
using RestApp.Helper;

namespace RestApp.Controllers
{
    // This Controler accepts xml as input data
    [Route("api/SecondCompany")]
    public class SecondCompanyController : ApiController
    {
        // GET api/SecondCompany
        public string Get()
        {
            return "This is Second Company API";
        }

        // GET api/SecondCompany/5
        public string Get(int id)
        {
            return "Second Company API, entered Param : " + id;
        }

        // POST api/AskShippingPrice
        [HttpPost, Route("api/SecondCompany/AskShippingPrice")]
        public IHttpActionResult EstimateShippingPrice([FromBody]ShippingOrder inputData)
        {

            string outPutResult = string.Empty;
            try
            {
                if (inputData == null)
                {
                    return BadRequest("FirstCompany api, Invalid request, no data");
                }
                // Calculate the shipping price according to input data
                float price = CalculateShippingPriceFromInput(inputData);

                Object result2 = JsonConvert.DeserializeObject(price.ToString());
                return Ok(result2);


            }
            catch (Exception e)
            {
                return BadRequest("SecondCompany api, Error in web service :" + e.Message);
            }
            
        }

        private float CalculateShippingPriceFromInput(ShippingOrder shippingOrder)
        {
            // calculate the price according to given data 
            return 15;
        }

    }
}
