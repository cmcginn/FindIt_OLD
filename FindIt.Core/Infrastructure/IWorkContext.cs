using FindIt.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindIt.Core.Infrastructure
{
    public interface IWorkContext
    {
        User User { get; set; }
    }
}
