//using FindIt.Core.Entities;
//using Raven.Abstractions.Indexing;
//using Raven.Client.Indexes;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace FindIt.Data.Indexes
//{
//    public class StateProvinceCities_OLD : AbstractIndexCreationTask<Country, CodeName>
//    {
       

//        public StateProvinceCities_OLD()
//        {
//            Map = countries => from country in countries
//                                    from stateProvince in country.StateProvinces
//                                    from city in stateProvince.Cities
//                                    select new CodeName
//                                    {
//                                        Code = String.Format("{0}/{1}/{2}",country.CountryCode,stateProvince.StateProvinceCode,city.CityName),
//                                        Name = city.CityName
//                                    };
//            Reduce = results => from result in results
//                                group result by result.Code
//                                    into g
//                                    select new CodeName
//                                    {
//                                        Code = g.Key,
//                                        Name = g.First().Name
//                                    };

//            Indexes.Add(x => x.Code, FieldIndexing.Analyzed);
//        }
//    }
//}
