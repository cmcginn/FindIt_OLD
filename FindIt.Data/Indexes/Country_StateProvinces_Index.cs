using FindIt.Core.Entities;
using Raven.Client.Indexes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindIt.Data.Indexes
{

    public class Country_StateProvinces_Index : AbstractIndexCreationTask<Country>
    {
      
        public Country_StateProvinces_Index()
        {

            Map = countries =>
                from country in countries
                from sp in country.StateProvinces
                select new { sp.Name };

            
            
        }
    }
}
