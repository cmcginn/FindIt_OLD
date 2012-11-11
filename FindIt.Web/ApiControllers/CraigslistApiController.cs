using FindIt.Core.Entities;
using FindIt.Data;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace FindIt.Web.ApiControllers
{
    public class CraigslistApiController : ApiController
    {
        private readonly IStorage _storage;
        public CraigslistApiController(IStorage storage)
        {
            _storage = storage;
        }
        [AcceptVerbs("GET", "HEAD")]
        public object CraigslistGroups()
        {
            object result = null;
            using (var store = _storage.DocumentStore)
            using (var session = store.OpenSession())
            {
                var query = session.Query<CraigslistGroup>();
                result = query.ToList();
            }
            return result;
        }
        [AcceptVerbs("GET", "HEAD")]
        public object GetCategories(string groupId)
        {
            object result = null;
            using (var store = _storage.DocumentStore)
            using (var session = store.OpenSession())
            {
                var query = session.Query<CraigslistCategory>().Where(x => x.Group.Id == groupId);
                result = query.ToList();
            }
            return result;
        }
        [AcceptVerbs("POST")]
        public void SaveCity(JObject data)
        {
            var stateProvinceCode = data["StateProvinceCode"].Value<string>();
            var cityName = data["CityName"].Value<string>();
            var selected = data["Selected"].Value<bool>();
           
        }
        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        //// GET api/<controller>
        //public IEnumerable<StateProvince> GetStates()
        //{
        //    List<StateProvince> result = null;
        //    using (var store = _storage.DocumentStore)
        //    using (var session = store.OpenSession())
        //    {
        //        result = from 
        //    }
        //    return result;
        //}

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