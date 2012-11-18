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
    public class Countries:AbstractIndexCreationTask<Country,CodeName>
    {

   
        public Countries()
        {
            Map = countries => from country in countries
                               select new CodeName
                               {
                                   Code = country.CountryCode,
                                   Name = country.CountryName
                               };

            Reduce = results => from country in results
                                group country by country.Code
                                    into g
                                                        select new CodeName
                                                        {
                                                            Code = g.Key,
                                                            Name = g.First().Name
                                                        };
            Indexes.Add(x => x.Code, FieldIndexing.Analyzed);
        }
    }
}
