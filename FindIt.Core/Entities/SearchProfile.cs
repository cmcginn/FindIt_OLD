using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindIt.Core.Entities
{
    public class SearchProfile:BaseEntity
    {
        Dictionary<string, string> _SearchLocations;

        public Dictionary<string, string> SearchLocations
        {
            get { return _SearchLocations??(new Dictionary<string,string>()); }
            set { _SearchLocations = value; }
        }
        public string Name { get; set; }
        
    }
}
