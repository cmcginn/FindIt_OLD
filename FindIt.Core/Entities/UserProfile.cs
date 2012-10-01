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
        ICollection<SearchProfile> _SearchProfiles;
        public ICollection<SearchProfile> SearchProfiles
        {
            get { return _SearchProfiles ?? (_SearchProfiles = new List<SearchProfile>()); }
            set { _SearchProfiles = value; }
        }
    }
}
