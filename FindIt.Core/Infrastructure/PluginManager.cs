using Autofac;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindIt.Core.Infrastructure
{
    public abstract class PluginManager
    {
        string _PluginPath;
        protected string PluginPath
        {
            get
            {
                if (String.IsNullOrEmpty(_PluginPath))
                {
                    _PluginPath = System.Configuration.ConfigurationManager.AppSettings["PluginPath"];
                }
                return _PluginPath;
            }
            set
            {
                _PluginPath = value;
            }
        }
        private Dictionary<string, string> _InstalledPluginDictionary;

        protected Dictionary<string, string> InstalledPluginDictionary
        {
            get { return _InstalledPluginDictionary; }
            set { _InstalledPluginDictionary = value; }
        }
        public abstract void InitializePlugins();
      
    }
}
