using FindIt.Core.Entities;
using Raven.Client.Indexes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Raven.Abstractions.Indexing;
namespace FindIt.Data.Indexes
{
    public class Country_StateProvince : AbstractIndexCreationTask<Country, Country_StateProvince.ReduceResult>
    {
        public class ReduceResult
        {
            public string CountryId { get; set; }
            public string CountryCode { get; set; }            
            public string StateProvinceCode { get; set; }
            public string StateProvinceName { get; set; }

        }

        public Country_StateProvince()
        {
            Map = countries => from country in countries
                               from stateProvince in country.StateProvinces
                               select new ReduceResult 
                               {
                                   CountryId = country.Id,
                                   CountryCode = country.CountryCode,                                   
                                   StateProvinceCode = stateProvince.StateProvinceCode,
                                   StateProvinceName = stateProvince.StateProvinceName
                              
                               };


            Reduce = results => from result in results
                                group result by result.StateProvinceCode
                                into g
                                select new 
                                {
                                    CountryId = g.First().CountryId,
                                    CountryCode = g.First().CountryCode,
                                    StateProvinceCode = g.Key,
                                    StateProvinceName = g.First().StateProvinceName                                   
                                };
            //Store("CountryCode", FieldStorage.Yes);
            //Store("StateProvinceCode",FieldStorage.Yes);
            //Index("Country", FieldIndexing.Analyzed);
            //Index("StateProvince", FieldIndexing.Analyzed);
        }
    }
}
