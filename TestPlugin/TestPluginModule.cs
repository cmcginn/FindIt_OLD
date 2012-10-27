using FindIt.Core.Plugins;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace TestPlugin
{
    public class TestPluginModule:IModule
    {
        public string Title
        {
            get { return "TestPlugin"; }
        }

        public string Name
        {
            get { return Assembly.GetAssembly(GetType()).GetName().Name; }
        }

        public Version Version
        {
            get { return new Version(1, 0, 0, 0); }
        }

        public string EntryControllerName
        {
            get { return "TestPlugin"; }
        }
    }
}
