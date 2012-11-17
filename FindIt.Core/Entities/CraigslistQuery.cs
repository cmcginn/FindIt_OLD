using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindIt.Core.Entities
{

    public class CraigslistQuery:BaseEntity
    {
        public virtual string UserId { get; set; }
        public virtual List<StateProvince> States { get; set; }
        public virtual List<CraigslistGroup> Groups { get; set; }
        public virtual List<Keyword> Keywords { get; set; }
        public virtual string QueryName { get; set; }
        public class Keyword
        {
            public string KeywordName { get; set; }
            public int KeywordScore { get; set; }
        }
    }
}
