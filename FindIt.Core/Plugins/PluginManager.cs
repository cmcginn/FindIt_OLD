using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace FindIt.Core.Plugins
{
    public class PluginManager
    {
        public PluginManager()
        {
            Modules = new Dictionary<IModule, Assembly>();
        }

        private static PluginManager _current;
        public static PluginManager Current
        {
            get { return _current ?? (_current = new PluginManager()); }
        }

        public Dictionary<IModule, Assembly> Modules { get; set; }

        public IEnumerable<IModule> GetModules()
        {
            return Modules.Select(m => m.Key).ToList();
        }

        public IModule GetModule(string name)
        {
            return GetModules().Where(m => m.Name == name).FirstOrDefault();
        }
    }
}
