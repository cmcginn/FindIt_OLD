using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindIt.Core.Entities
{
    public class InstalledPlugin:BaseEntity
    {
        public virtual string FriendlyName { get; set; }
        public virtual string SystemName { get; set; }
        public virtual string Version { get; set; }
        public virtual string AssemblyName { get; set; }
        
    }
}
