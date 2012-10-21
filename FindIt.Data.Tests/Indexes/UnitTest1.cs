using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Raven.Client.Document;
using Raven.Client.Indexes;
using FindIt.Data.Indexes;
using FindIt.Core.Entities;


namespace FindIt.Data.Tests.Indexes
{
    [TestClass]
    public class UnitTest1
    {
        //[TestInitialize]
        //public void Initialize()
        //{
        //    using (var documentStore = new DocumentStore())
        //    {
        //        documentStore.Url = "http://Windows8:8080";
        //        documentStore.DefaultDatabase = "Test";
        //        documentStore.Initialize();
        //        IndexCreation.CreateIndexes(typeof(Country_StateProvinces_Index).Assembly, documentStore);

        //    }
        //}
        //[TestCleanup]
        //public void Cleanup()
        //{
        //    //using (var documentStore = new DocumentStore())
        //    //{
        //    //    documentStore.Url = "http://Windows8:8080";
        //    //    documentStore.DefaultDatabase = "Test";
        //    //    documentStore.Initialize();
        //    //    documentStore.DatabaseCommands.DeleteIndex("Country/StateProvinces/Index");
        //    //}
        //}
        //[TestMethod]
        //public void TestIndexCreation()
        //{
        //    using (var documentStore = new DocumentStore())
        //    {
        //        documentStore.Url = "http://Windows8:8080";
        //        documentStore.DefaultDatabase = "Test";
        //        documentStore.Initialize();
        //        using (var session = documentStore.OpenSession())
        //        {
        //            var query = session.Query<Country, Country_StateProvinces_Index>();
        //            var x = query.ToList();

        //            //var x = query.T


        //        }
        //    }
        //}
        [TestMethod]
        public void RemoteConnectionTest()
        {
            using (var documentStore = new DocumentStore { ConnectionStringName = "RavenDB" })
            {
                documentStore.DefaultDatabase = "Test";
                documentStore.Initialize();
                using (var session = documentStore.OpenSession())
                {
                    var country = new Country { CountryName = "Test" };
                    session.Store(country);
                    session.SaveChanges();
                }
            }
        }
    }
}
