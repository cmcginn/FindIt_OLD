using System;
using System.Collections.Generic;
namespace FindIt.Core.Infrastructure
{
    public interface IPluginManager
    {
        void InitializePlugins();
        Dictionary<string, string> InstalledPluginDictionary { get; }
    }
}
