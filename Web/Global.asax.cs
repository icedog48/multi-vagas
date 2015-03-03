using Migrations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Optimization;
using System.Web.Routing;
using Web.App_Start;
using Web.DependencyResolution.Registries;

namespace Web
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            var container = StructuremapMvc.StructureMapDependencyScope.Container;

            MigrationsRunner.Run(container.GetInstance<string>("DefaultConnection"));

            container.Configure(c => c.AddRegistry(new NHibernateRegistry(StructuremapMvc.StructureMapDependencyScope.Container)));

            GlobalConfiguration.Configure(WebApiConfig.Register);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            StructuremapWebApi.Start();

            Automapper.Setup();
        }

        protected void Application_EndRequest()
        {
            
        }
    }
}
