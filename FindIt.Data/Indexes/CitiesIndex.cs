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
    public class CitiesIndex : AbstractIndexCreationTask<City, City>
    {
        public CitiesIndex()
        {
            Map = cities => from doc in cities
                            select new { doc.CityName };
            Stores.Add(x => x.CityName,FieldStorage.Yes);
            Indexes.Add(x => x.CityName, FieldIndexing.Analyzed);
        }
    }
}
