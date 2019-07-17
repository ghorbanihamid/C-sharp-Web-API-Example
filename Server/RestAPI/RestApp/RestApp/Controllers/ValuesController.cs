using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestApp.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace RestApp.Controllers
{
    public class ValuesController : ApiController
    {
        // GET api/values
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }

        [HttpPost, Route("api/values/TestWebService")]
        public IHttpActionResult TestWebService([FromBody]CompanyInfo inputData)
        {

            string outPutResult = string.Empty;
            try
            {
                if (inputData == null)
                {
                    return BadRequest("TestWebService api, Invalid request, input data is null");
                }
                outPutResult = @"{'result':TestWebService is ok}";

                Object result1 = JObject.Parse(outPutResult);
                Object result2 = JsonConvert.DeserializeObject(outPutResult);
                return Ok(result2);
            }
            catch (Exception e)
            {
                return BadRequest("TestWebService api, Error in web service :" + e.Message);
            }

        }
    }
}
