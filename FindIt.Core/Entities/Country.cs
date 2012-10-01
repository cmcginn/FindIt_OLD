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
        ICollection<StateProvince> _StateProvinces;

        public ICollection<StateProvince> StateProvinces
        {
            get { return _StateProvinces ?? (_StateProvinces = new List<StateProvince>()); }
            set { _StateProvinces = value; }
        }
        
    }
}
