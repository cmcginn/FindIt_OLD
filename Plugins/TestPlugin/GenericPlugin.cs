using FindIt.Core.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestPlugin
{
    [PluginName("GenericPlugin")]
    public class GenericPlugin : QueryPluginBase
    {
        public GenericPlugin()
        {
            this._FriendlyName = "Generic Plugin";
            this._SystemName = "GenericPlugin";
            this._AssemblyName = "TestPlugin.dll";
        }
        public override void Install()
        {
            throw new NotImplementedException();
        }

        public override void Uninstall()
        {
            throw new NotImplementedException();
        }
    }

}

