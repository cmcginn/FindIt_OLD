using FindIt.Core.Entities;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace FindIt.Web.ApiControllers
{
    public class MockApiController : ApiController
    {
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
        public object GetStateProvinceSearch()
        {
            var model = new 
            {
                States=new object[]{
                    new {StateName="Alabama", StateId="states/alabama", CityCount=5},
                    new {StateName="Arkansas", StateId="states/arkansas", CityCount=3},
                    new {StateName="Alaska", StateId="states/alaska", CityCount=1},
                    new {StateName="California", StateId="states/california", CityCount=100}
                }
            };
            return model;
        }

        public List<StateProvince> GetAllStateProvinces()
        {
            var result = new List<StateProvince>
            {
                new StateProvince{ StateProvinceCode="/stateprovinces/1", StateProvinceName="Alabama"},
                new StateProvince{ StateProvinceCode="/stateprovinces/2", StateProvinceName="Alaska"},
                new StateProvince{ StateProvinceCode="/stateprovinces/3", StateProvinceName="Arkansas"},
                new StateProvince{ StateProvinceCode="/stateprovinces/4", StateProvinceName="California"},
                new StateProvince{ StateProvinceCode="/stateprovinces/5", StateProvinceName="Colorado"},
                new StateProvince{ StateProvinceCode="/stateprovinces/6", StateProvinceName="Delaware"},
                new StateProvince{ StateProvinceCode="/stateprovinces/7", StateProvinceName="Florida"},
                new StateProvince{ StateProvinceCode="/stateprovinces/8", StateProvinceName="Georgia"},
                new StateProvince{ StateProvinceCode="/stateprovinces/9", StateProvinceName="Hawaii"}

            };
            return result;
        }
    }

    #region mock models
  //  public class StateSeaerch
    #endregion
}