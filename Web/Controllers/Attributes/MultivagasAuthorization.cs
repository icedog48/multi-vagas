using Model;
using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Http;
using System.Web.Http.Filters;
using Web.App_Start;

namespace Web.Controllers.Attributes
{
    public class MultivagasAuthorizeAttribute : AuthorizeAttribute
    {
        public override void OnAuthorization(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            if (HttpContext.Current.User.Identity.IsAuthenticated)
            {
                var container = StructuremapMvc.StructureMapDependencyScope.Container;
                
                var usuarioService = container.GetInstance<IUsuarioService>();

                var claimIdentity = (ClaimsIdentity)HttpContext.Current.User.Identity;

                var usuario = usuarioService.GetByEmail(claimIdentity.FindFirst(ClaimTypes.Email).Value);

                //Coloca uma instancia do objeto Usuario disponivel para ser injetado nos serviços            
                container.Configure(c =>
                {
                    c.For<Usuario>().Use(usuario).Named("usuarioLogado");
                });
            }

            base.OnAuthorization(actionContext);
        }

    }
}