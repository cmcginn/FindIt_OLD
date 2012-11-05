using FindIt.Core.Entities;
using Raven.Client.Indexes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindIt.Data.Indexes
{
    public class StateProvince_City:AbstractIndexCreationTask<Country,StateProvince_City.ReduceResult>
    {
        public class ReduceResult
        {
            public string StateProvinceCode { get; set; }
            public List<City> Cities { get; set; }
        }
        public StateProvince_City()
        {
            Map = countries => from country in countries
                               from stateProvince in country.StateProvinces
                               from cities in stateProvince.Cities
                               select new
                               {
                                   StateProvinceCode = stateProvince.StateProvinceCode,
                                   Cities = cities
                               };
            Reduce = results => from result in results
                                group result by result.StateProvinceCode
                                    into g
                                    select new
                                    {
                                        StateProvinceCode = g.Key,
                                        Cities = g.Select(x=>x.Cities)
                                    };

        }
    }
}
