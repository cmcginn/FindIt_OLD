using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FindIt.Data;
using System.Linq;
using FindIt.Core.Entities;
namespace FindIt.Data.Tests
{
    [TestClass]
    public class DataTests
    {
        static string wellKnownUserId = "129";
        [TestMethod]
        public void GetUserProfileTest()
        {
            using(var store = Storage.GetStore)
            using (var session = store.OpenSession())
            {
                var query = session.Query<UserProfile>().Single(x => x.UserId == String.Format("users/{0}",wellKnownUserId));
                var profile = query;
                Assert.IsInstanceOfType(profile, typeof(UserProfile));
            }
        }
    }
}
