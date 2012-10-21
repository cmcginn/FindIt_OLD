using FindIt.Core.Entities;
using FindIt.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace FindIt.Web.ApiControllers
{
    public class LocationController : ApiController
    {
        private readonly IStorage _storage;

        public LocationController(IStorage storage)
        {
            _storage = storage;
        }

        [AcceptVerbs("GET", "HEAD")]
        public IEnumerable<City> StateProvinceCity(string name)
        {
            List<City> result = null;
            using (var store = _storage.DocumentStore)
            using (var session = store.OpenSession())
            {
                result = session.Advanced.LuceneQuery<City>().Search("StateCode", String.Format("{0}", name)).GroupBy(x=>x.CityName).Select(x=>x.First()).ToList();
            }
            return result;
        }
        [AcceptVerbs("GET", "HEAD")]
        public IEnumerable<StateProvince> StateProvince(string name)
        {
            List<StateProvince> result = null;
            using (var store = _storage.DocumentStore)
            using (var session = store.OpenSession())
            {
                result = session.Query<StateProvince>().Where(x => x.StateProvinceName.StartsWith(name) || x.StateProvinceCode.StartsWith(name)).ToList();
            }
            return result;
        }
        [AcceptVerbs("GET", "HEAD")]
        public IEnumerable<Country> Country(string name)
        {
            List<Country> result = null;
            using (var store = _storage.DocumentStore)
            using (var session = store.OpenSession())
            {
                result = session.Query<Country>().Where(x => x.CountryName.StartsWith(name) || x.CountryCode.StartsWith(name)).ToList();
            }
            return result;
        }
        [AcceptVerbs("GET", "HEAD")]
        public IEnumerable<Country> AllCountries()
        {
            List<Country> result = null;
            using (var store = _storage.DocumentStore)
            using (var session = store.OpenSession())
            {
                result = session.Query<Country>().ToList();
            }
            return result;
        }

        [AcceptVerbs("GET", "HEAD")]
        public IEnumerable<StateProvince> CountryStateProvince(string name)
        {
            List<StateProvince> result = null;
            using (var store = _storage.DocumentStore)
            using (var session = store.OpenSession())
            {
                result = session.Query<StateProvince>().Where(x => x.CountryCode == name).ToList();
            }
            return result;
        }

        // GET api/<controller>
        public IEnumerable<Country> Get()
        {
            IEnumerable<Country> result = null;
            
            using(var store = _storage.DocumentStore)
            using (var session = store.OpenSession())
            {
                result = session.Query<Country>().ToList();
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