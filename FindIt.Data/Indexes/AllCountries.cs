using FindIt.Core.Entities;
using Raven.Client.Indexes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindIt.Data.Indexes
{
    public class AllCountries : AbstractIndexCreationTask<Location>
    {
        public AllCountries()
        {
            Map = locations => from location in locations.Where(x => x.Id.Length == 2)
                               select new Location
                               {
                                   Id = location.Id,
                                   Name = location.Name
                               };
        }
    }
}
