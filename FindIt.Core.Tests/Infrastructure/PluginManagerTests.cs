using Autofac;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FindIt.Core.Infrastructure;

namespace FindIt.Core.Tests.Infrastructure
{
    [TestClass]
    public class PluginManagerTests
    {
        TestablePluginManager Target
        {
            get
            {
                return new TestablePluginManager();
            }
        }
        [TestMethod]
        public void InitializePluginsTest()
        {
            var target = Target;
            target.InitializePlugins();
            Assert.IsTrue(target._AssignableTypes.Count == 1);
            var plugin = target._Container.ResolveNamed("GenericPlugin", typeof(IPlugin)) as IPlugin;
            Assert.IsInstanceOfType(plugin, typeof(IPlugin));
            Assert.IsTrue(plugin.FriendlyName == "Generic Plugin");
        }
    }
}
