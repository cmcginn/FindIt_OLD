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
       
        public virtual string Name { get; set; }
        public virtual string ThreeLetterISOCode { get; set; }
        public virtual string TwoLetterISOCode { get; set; }
        
        
    }
}
