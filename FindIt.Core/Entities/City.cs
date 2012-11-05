using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindIt.Core.Entities
{
    public class City : BaseEntity
    {   
        
        public virtual string CityName { get; set; }        
        public virtual string CommunityName { get; set; }

    }
}
