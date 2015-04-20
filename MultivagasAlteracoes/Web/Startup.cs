using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Owin;
using Service.Interfaces;
using StructureMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Optimization;
using System.Web.Routing;
using Web;
using Web.App_Start;
using Web.DependencyResolution.Registries;
using WebApiAuthentication.Providers;

[assembly: OwinStartup(typeof(Web.Startup))]
namespace Web
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var container = StructuremapMvc.StructureMapDependencyScope.Container;
            MigrationsRunner.Run(container); // Executa o migrations com os parametros do container configurados

            //Adiciona as configurações do NHibernate para criação de sessão, repositories etc
            container.Configure(c => c.AddRegistry(new NHibernateRegistry(container)));

            ConfigureOAuth(app, container);

            GlobalConfiguration.Configure(WebApiConfig.Register);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            Automapper.Setup();
            
            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
        }

        public void ConfigureOAuth(IAppBuilder app, IContainer container)
        {
            OAuthAuthorizationServerOptions OAuthServerOptions = new OAuthAuthorizationServerOptions()
            {
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(1),
                Provider = new SimpleAuthorizationServerProvider(container.GetInstance<IUsuarioService>())
            };

            // Token Generation
            app.UseOAuthAuthorizationServer(OAuthServerOptions);
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());
        }
    }
}