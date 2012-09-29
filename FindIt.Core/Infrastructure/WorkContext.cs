using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindIt.Core.Infrastructure
{
    public class WorkContext:IWorkContext
    {
        public Entities.User User { get; set; }
       
    }
}
