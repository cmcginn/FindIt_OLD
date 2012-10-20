using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindIt.Core.Entities
{
    public class UserProfile:BaseEntity
    {
        
        public string UserId { get; set; }
        ICollection<Query> _Queries;
        public ICollection<Query> Queries
        {
            get { return _Queries ?? (_Queries = new List<Query>()); }
            set { _Queries = value; }
        }
    }
}
