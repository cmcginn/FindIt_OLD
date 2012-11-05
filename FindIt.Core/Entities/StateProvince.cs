using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindIt.Core.Entities
{
    
    public class StateProvince:BaseEntity
    {        
        public string StateProvinceName { get; set; }
        public string StateProvinceCode { get; set; }
        public List<City> Cities { get; set; }
    }
}
