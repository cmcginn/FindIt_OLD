
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindIt.Core.Entities
{
    public class ApplicationSession:BaseEntity
    {
        public string UserId { get; set; }
        public string SessionData { get; set; }
    }
}
