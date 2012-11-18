using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using FindIt.Core.Entities;
using FindIt.Core.Infrastructure;
using FindIt.Data;
using FindIt.Web.Infrastructure;
using RazorGenerator.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.WebPages;


namespace FindIt.Web
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {

        protected void Application_Start()
        {


            var engineContext = EngineContext.CurrentContext;            
            engineContext.Builder.RegisterControllers(typeof(MvcApplication).Assembly);
            engineContext.Builder.RegisterType<Storage>().As<IStorage>().InstancePerHttpRequest();
            engineContext.Builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            
            engineContext.RegisterTypes();


            EngineContext.Container = engineContext.Builder.Build();

            DependencyResolver.SetResolver(new AutofacDependencyResolver(EngineContext.Container));
            GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver(EngineContext.Container);

            FindItPluginBootstrapper.Initialize();
  
            AreaRegistration.RegisterAllAreas();
            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AuthConfig.RegisterAuth();

            EngineContext.Container.Resolve<IWorkContext>().User = new User { Id = "users/33", UserName="Static Test User" };
        }
    }
}