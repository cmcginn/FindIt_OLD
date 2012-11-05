using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindIt.Core.Entities
{
    public class CraigslistCity:BaseEntity
    {
        public virtual City City { get; set; }
        public virtual string BaseUrl { get; set; }
        public bool Active { get; set; }
    }
}
