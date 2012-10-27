using FindIt.Core.Infrastructure;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Compilation;
using System.Web.Hosting;
using Autofac;
using Autofac.Integration.Mvc;
[assembly: PreApplicationStartMethod(typeof(FindIt.Core.Plugins.PreApplicationInit), "InitializePlugins")]
namespace FindIt.Core.Plugins
{
    public class PreApplicationInit
    {
        static PreApplicationInit()
        {
            string pluginsPath = HostingEnvironment.MapPath("~/plugins");
            string pluginsTempPath = HostingEnvironment.MapPath("~/plugins/temp");

            if (pluginsPath == null || pluginsTempPath == null)
                throw new DirectoryNotFoundException("plugins");

            PluginFolder = new DirectoryInfo(pluginsPath);
            TempPluginFolder = new DirectoryInfo(pluginsTempPath);
        }

        /// <summary>
        /// The source plugin folder from which to copy from
        /// </summary>
        /// <remarks>
        /// This folder can contain sub folders to organize plugin types
        /// </remarks>
        private static readonly DirectoryInfo PluginFolder;

        /// <summary>
        /// The folder to  copy the plugin DLLs to use for running the app
        /// </summary>
        private static readonly DirectoryInfo TempPluginFolder;

        /// <summary>
        /// Initialize method that registers all plugins
        /// </summary>
        public static void InitializePlugins()
        {
            Directory.CreateDirectory(TempPluginFolder.FullName);

            //clear out plugins
            foreach (var f in TempPluginFolder.GetFiles("*.dll", SearchOption.AllDirectories))
            {
                try
                {
                    f.Delete();
                }
                catch (Exception)
                {

                }

            }

            //copy files
            foreach (var plug in PluginFolder.GetFiles("*.dll", SearchOption.AllDirectories))
            {
                try
                {
                    var di = Directory.CreateDirectory(TempPluginFolder.FullName);
                    File.Copy(plug.FullName, Path.Combine(di.FullName, plug.Name), true);
                }
                catch (Exception)
                {

                }
            }
           
            // * This will put the plugin assemblies in the 'Load' context
            // This works but requires a 'probing' folder be defined in the web.config
            // eg: <probing privatePath="plugins/temp" />
            var assemblies = TempPluginFolder.GetFiles("*.dll", SearchOption.AllDirectories)
                    .Select(x => AssemblyName.GetAssemblyName(x.FullName))
                    .Select(x => Assembly.Load(x.FullName));

            foreach (var assembly in assemblies)
            {
                Type type = assembly.GetTypes().Where(t => t.GetInterface(typeof(FindIt.Core.Plugins.IModule).FullName) != null).FirstOrDefault();
                if (type != null)
                {
                    //Add the plugin as a reference to the application
                    BuildManager.AddReferencedAssembly(assembly);
                    EngineContext.CurrentContext.Builder.RegisterControllers(assembly);
                    //Add the modules to the PluginManager to manage them later
                    var module = (IModule)Activator.CreateInstance(type);
                    PluginManager.Current.Modules.Add(module, assembly);
                }
            }
        }
    }
}
