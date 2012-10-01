using FindIt.Core.Entities;
using Raven.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindIt.Data.Extensions
{
    public static class QueryExtensions
    {
        public static UserProfile UserProfileByUserId(this IDocumentSession session,string id)
        {
            return session.Query<UserProfile>().Single(x => x.UserId == id);
        }

        public static List<Country> GetCountries(this IDocumentSession session)
        {
            return session.Query<Country>().ToList();
        }
    }

    
}
