using FindIt.Core.Entities;
using Raven.Abstractions.Indexing;
using Raven.Client.Indexes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindIt.Data.Indexes
{
    public class CountryStateProvinces : AbstractIndexCreationTask<Country,CodeName>
    {
       
        public CountryStateProvinces()
        {
            Map = countries => from country in countries
                               from stateProvince in country.StateProvinces                               
                               select new CodeName
                               {
                                   Code = String.Format("{0}/{1}", country.CountryCode, stateProvince.StateProvinceCode),
                                   Name = stateProvince.StateProvinceName
                               };
            Reduce = results => from result in results
                                group result by result.Code
                                    into g
                                    select new
                                    {
                                        Code = g.Key,
                                        Name = g.First().Name
                                    };

            Indexes.Add(x => x.Code, FieldIndexing.Analyzed);
        }
    }
}
