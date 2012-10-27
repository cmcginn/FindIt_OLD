using FindIt.Core.Plugins;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FindIt.Web.Infrastructure
{
    public static class FindItPluginBootstrapper
    {
        static FindItPluginBootstrapper()
        {
	        
        }

    	public static void Initialize()
        {
            foreach (var asmbl in PluginManager.Current.Modules.Values)
	        {
                BoC.Web.Mvc.PrecompiledViews.ApplicationPartRegistry.Register(asmbl);
	        }
        }
    }
}