using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindIt.Core.Infrastructure
{
    public interface IPlugin
    {
        string SystemName { get; set; }
        string FriendlyName { get; set; }
        string AssemblyName { get; set; }
        void Install();
        void Uninstall();
    }
}
