using FindIt.Core.Entities;
using FindIt.Core.Infrastructure;
using FindIt.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using FindIt.Core.Extensions;
using FindIt.Data.Indexes;
namespace FindIt.Web.ApiControllers
{
    public class QueryProfileApiController : ApiController
    {
        private readonly IStorage _storage;
        private readonly IWorkContext _workContext;
        public QueryProfileApiController(IStorage storage, IWorkContext workContext)
        {
            _storage = storage;
            _workContext = workContext;
        }

        [AcceptVerbs("GET", "HEAD")]
        public QueryProfile GetNewQueryProfile()
        {
            QueryProfile result = null;
            using (var store = _storage.DocumentStore)
            using (var session = store.OpenSession())
            {
                var query = session.Query<QueryProfile>().Where(x => x.UserId == _workContext.User.Id).Select(x => x.Name).ToList();
                var newName = query.UniqueName("New Query Profile");

                result = new QueryProfile { UserId = _workContext.User.Id, Name = "New Query Profile" };
                session.Store(result);
                session.SaveChanges();

            }
            return result;
        }
        [AcceptVerbs("POST")]
        public QueryProfile SaveQueryProfileName(string id, string name)
        {
            QueryProfile result = null;
            using (var store = _storage.DocumentStore)
            using (var session = store.OpenSession())
            {
                try
                {
                    var query = session.Query<QueryProfile>().Where(x => x.UserId == _workContext.User.Id && x.Name == name).Select(x => x.Name).ToList();
                    var uniqueName = query.UniqueName("New Query Profile");
                    result = session.Load<QueryProfile>(id);
                    result.Name = uniqueName;
                    session.SaveChanges();
                }
                catch
                {
                    throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.NotFound));
                }
            }
            return result;
        }
        [AcceptVerbs("GET", "HEAD")]
        public List<Location> GetCountries()
        {
            List<Location> result = null;
            using (var store = _storage.DocumentStore)
            using (var session = store.OpenSession())
            {
                var query = session.Query<CodeName, Countries>();
                result = query.ToList();
            }
            return result;
        }
        [AcceptVerbs("GET", "HEAD")]
        public List<CodeName> GetLocations(string code)
        {
            List<CodeName> result = null;
            try
            {

                using (var store = _storage.DocumentStore)
                using (var session = store.OpenSession())
                {
                    var qString = String.Format("{0}/",code);
                    var codeIndex = code.Split('/');
                    if (codeIndex.Count() == 1)
                        result = session.Advanced.LuceneQuery<CodeName, FindIt.Data.Indexes.CountryStateProvinces>().Search("Code", qString).ToList();
                    else if (codeIndex.Count() == 2)
                        result = session.Advanced.LuceneQuery<CodeName, FindIt.Data.Indexes.StateProvinceCities_OLD>().Search("Code", qString).ToList();
                   
                }
            }
            catch
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.NotFound));
            }
            return result;
        }
        [AcceptVerbs("POST")]
        public HttpResponseMessage SaveLocation(string profileId,string code)
        {
            try
            {

                using (var store = _storage.DocumentStore)
                using (var session = store.OpenSession())
                {
                    var profile = session.Load<QueryProfile>(profileId);
                    var location = GetLocations(code);
                    if(location.Count==0)
                        throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.NotFound));
                    if (profile.Locations.Count(x => x == code) == 0)
                    {
                        profile.Locations.Add(code);
                        session.SaveChanges();
                        return Request.CreateResponse<string>(HttpStatusCode.Created, code);
                    }

                    return Request.CreateResponse(HttpStatusCode.NotModified);
                }
            }
            catch
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.NotFound));
            }
        }

    }
}