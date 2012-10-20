using FindIt.Core.Entities;
using FindIt.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using FindIt.Data.Extensions;
namespace FindIt.Web.ApiControllers
{
    public class CommonApiController : ApiController
    {

        private readonly IStorage _storage;
        public CommonApiController(IStorage storage)
        {
            _storage = storage;
        }

        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }
        [AcceptVerbs("GET", "HEAD")]
        public IEnumerable<Country> GetCountries()
        {
            List<Country> result = null;
            using(var store = _storage.DocumentStore)
            using (var session = store.OpenSession())
            {
                result = session.GetCountries();
            }
            return result;
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