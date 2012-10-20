using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using FindIt.Core.Infrastructure;
using FindIt.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace FindIt.Web
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
       
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AuthConfig.RegisterAuth();
            var engineContext = EngineContext.CurrentContext;
            engineContext.Builder = new ContainerBuilder();            
            engineContext.Builder.RegisterControllers(typeof(MvcApplication).Assembly);
            engineContext.Builder.RegisterApiControllers(typeof(System.Web.Http.ApiController).Assembly);
            engineContext.Builder.RegisterType<Storage>().As<IStorage>().InstancePerDependency();
            engineContext.RegisterTypes();
            EngineContext.Container = engineContext.Builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(EngineContext.Container));
            engineContext.Builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            //var json = GlobalConfiguration.Configuration.Formatters.JsonFormatter;
            //json.UseDataContractJsonSerializer = true;
        }

       
    }
}