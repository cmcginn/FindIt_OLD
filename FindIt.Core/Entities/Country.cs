using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindIt.Core.Entities
{

    public class Country:BaseEntity
    {       
        public virtual string CountryName { get; set; }
        public virtual string CountryCode { get; set; }
    }
}
