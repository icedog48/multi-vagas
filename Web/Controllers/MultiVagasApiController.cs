using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Web.ViewModels;
using Storage;
using AutoMapper;
using Newtonsoft.Json;
using Web.Controllers.Attributes;
using Model;
using Service.Interfaces;
using Service.Filters;
using Web.App_Start;
using FluentValidation;
using System.Text;
using Model.Common;
using System.Web;
using System.Security.Claims;


namespace Web.Controllers
{
    public class MultiVagasApiController<T> : ApiController where T : Entity
    {
        public MultiVagasApiController(IMultiVagasCRUDService<T> service)
        {
            var container = StructuremapMvc.StructureMapDependencyScope.Container;

            var usuarioLogado = container.GetInstance<Usuario>("usuarioLogado");

            if (HttpContext.Current.User.Identity.IsAuthenticated && (usuarioLogado == null || string.IsNullOrEmpty(usuarioLogado.Email)))
            {
                var usuarioService = container.GetInstance<IUsuarioService>();

                var claimIdentity = (ClaimsIdentity)HttpContext.Current.User.Identity;

                var usuario = usuarioService.GetByEmail(claimIdentity.FindFirst(ClaimTypes.Email).Value);

                //Coloca uma instancia do objeto Usuario disponivel para ser injetado nos serviços            
                container.Configure(c =>
                {
                    c.For<Usuario>().Use(usuario).Named("usuarioLogado");
                });
            }

            service.UsuarioLogado = container.GetInstance<Usuario>("usuarioLogado");
        }      
    }
}
