using FindIt.Core.Entities;
using FindIt.Core.Infrastructure;
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
    


    public class UserProfileApiController : ApiController
    {
        private readonly IWorkContext _workContext;
        public UserProfileApiController()
        {
            _workContext = EngineContext.CurrentContext.WorkContext;
        }
        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return null;
           
        }
        [AcceptVerbs("GET","HEAD")]
        public UserProfile GetCurrentProfile()
        {
            UserProfile result = null;
            using (var store = Storage.GetStore)
            using (var session = store.OpenSession())
            {
               result = session.UserProfileByUserId(_workContext.User.Id);               
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