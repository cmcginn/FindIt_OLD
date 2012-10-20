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
        public string Name { get; set; }
        public string Abbreviation { get; set; }
    }
}
