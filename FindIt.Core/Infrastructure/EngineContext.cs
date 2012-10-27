using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindIt.Core.Infrastructure
{
    public class EngineContext
    {
        static EngineContext _CurrentContext;
        static IContainer _Container;

        public static IContainer Container
        {
            get { return EngineContext._Container; }
            set { EngineContext._Container = value; }
        }
        ContainerBuilder _Builder;

        public ContainerBuilder Builder
        {
            get {

                return _Builder ?? (_Builder = new ContainerBuilder());
            }
            set { _Builder = value; }
        }
        private EngineContext() { }
       
        public static EngineContext CurrentContext
        {
            get
            {
                if (_CurrentContext == null)
                    _CurrentContext = new EngineContext();
                return _CurrentContext;
            }
        }

        public IWorkContext WorkContext
        {
            get
            {
                return _Container.Resolve<IWorkContext>();
            }
        }
        public void RegisterTypes()
        {
            _Builder.RegisterType<WorkContext>().As<IWorkContext>().SingleInstance();            
            
        }
        
    }
}
