using Autofac;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindIt.Core.Infrastructure
{
    public abstract class PluginManager : IPluginManager
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
        Dictionary<string, string> _InstalledPluginDictionary;

        public Dictionary<string, string> InstalledPluginDictionary
        {
            get { return _InstalledPluginDictionary ?? (_InstalledPluginDictionary = new Dictionary<string, string>()); }         
        }
        public abstract void InitializePlugins();
      
    }
}
