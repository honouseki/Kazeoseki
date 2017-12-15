using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Kazeoseki.Controllers.Api
{
    [RoutePrefix("api/echo")]
    public class EchoApiController : ApiController
    {
        [HttpGet, Route("{msg}")]
        public HttpResponseMessage EchoItBack(string msg)
        {
            return Request.CreateResponse(HttpStatusCode.OK, msg);
        }
        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}