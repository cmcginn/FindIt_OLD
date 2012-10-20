using Autofac;
using FindIt.Core.Infrastructure;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace FindIt.Core.Tests.Infrastructure
{
    public class TestablePluginManager:PluginManager
    {
        public ContainerBuilder _Builder;
        public IContainer _Container;
        public List<Type> _AssignableTypes;
        public override void InitializePlugins()
        {
            _Builder = new ContainerBuilder();
            //scan directory
            var di = new DirectoryInfo(PluginPath);
            List<Assembly> assemblies = new List<Assembly>();
            di.GetFiles("*Plugin.dll").ToList().ForEach(plugin =>
                {
                    assemblies.Add(Assembly.LoadFile(plugin.FullName));
                });
            _Builder.RegisterAssemblyTypes(assemblies.ToArray());
            


            var assemblyTypes = assemblies.ToList().SelectMany(x => x.GetTypes()).ToList();
            _AssignableTypes = assemblyTypes.Where(x => x.IsAssignableTo<IPlugin>()).ToList();
            _AssignableTypes.ForEach(at =>
                {            
                    var name = System.Attribute.GetCustomAttributes(at).OfType<FindIt.Core.Infrastructure.PluginName>().First().ToString();
                    _Builder.RegisterType(at).Named(name, typeof(IPlugin));
                });
            _Container = _Builder.Build();
          
        }
    }
}
