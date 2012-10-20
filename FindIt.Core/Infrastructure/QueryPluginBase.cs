using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindIt.Core.Infrastructure
{
    public abstract class QueryPluginBase:IPlugin
    {
        protected string _SystemName;

        public string SystemName
        {
            get { return _SystemName; }
            set { _SystemName = value; }
        }

        protected string _FriendlyName;

        public string FriendlyName
        {
            get { return _FriendlyName; }
            set { _FriendlyName = value; }
        }

        protected string _AssemblyName;

        public string AssemblyName
        {
            get { return _AssemblyName; }
            set { _AssemblyName = value; }
        }

        public abstract void Install();
        public abstract void Uninstall();
    }
}
