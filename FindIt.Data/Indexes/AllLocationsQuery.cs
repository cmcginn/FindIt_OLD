using FindIt.Core.Entities;
using Raven.Client.Indexes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindIt.Data.Indexes
{
    public class AllLocationsQuery : AbstractIndexCreationTask<Location,AllLocationsQuery.ReduceResult>
    {
        public class ReduceResult
        {
            public string LocationId { get; set; }
            //public string CountryId { get; set; }
            //public string StateProvinceId { get; set; }
            //public string CityId { get; set; }
        }

        public AllLocationsQuery()
        {
            Map = locations => from location in locations
                               select new ReduceResult
                               {
                                   LocationId = location.Id
                                  
                               };
            Reduce = results => from result in results
                                group result by result.LocationId 
                                    into g
                                    select new ReduceResult
                                    {
                                        LocationId = g.Key

                                    };
                              
        }
    }
}
