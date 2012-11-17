using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindIt.Core.Entities
{
    public class QueryProfile:BaseEntity
    {
        public string UserId { get; set; }
        public virtual string Name { get; set; }
    }
}
