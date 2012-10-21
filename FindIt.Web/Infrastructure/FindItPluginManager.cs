using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using FindIt.Core.Entities;
using FindIt.Core.Infrastructure;
using FindIt.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace FindIt.Web.Infrastructure
{
    public class FindItPluginManager:PluginManager
    {
        private readonly IStorage _storage;
        public FindItPluginManager(IStorage storage)
        {
            _storage = storage;
        }
        public override void InitializePlugins()
        {
            List<InstalledPlugin> installedPlugins = null;
            using (var store = _storage.DocumentStore)
            using (var session = store.OpenSession())
            {
                installedPlugins = session.Query<InstalledPlugin>().ToList();
            }

            var di = new DirectoryInfo(PluginPath);           
            List<Assembly> assemblies = new List<Assembly>();
            installedPlugins.ForEach(installedPlugin =>
                {
                    var fi = di.GetFiles(String.Format("{0}.dll", installedPlugins.First().AssemblyName)).First();
                    assemblies.Add(Assembly.LoadFile(fi.FullName));

                });
        
            EngineContext.CurrentContext.Builder.RegisterAssemblyTypes(assemblies.ToArray());
            var assemblyTypes = assemblies.ToList().SelectMany(x => x.GetTypes()).ToList();
            //var assignableTypes = assemblyTypes.Where(x => x.IsAssignableTo<IPlugin>()).ToList();
            assemblyTypes.ForEach(at =>
            {
                if (at.IsAssignableTo<IPlugin>())
                {
                    var name = System.Attribute.GetCustomAttributes(at).OfType<FindIt.Core.Infrastructure.PluginName>().First().ToString();
                    InstalledPluginDictionary.Add(name, at.ToString());
                    EngineContext.CurrentContext.Builder.RegisterType(at).Named(name, typeof(IPlugin));
                }
                else if (at.IsAssignableTo<Controller>())
                {
                    EngineContext.CurrentContext.Builder.RegisterControllers(at.Assembly).InstancePerHttpRequest();
                }
            });
        }
    }
}