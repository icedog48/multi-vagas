﻿using Microsoft.Owin.Security.OAuth;
using Model;
using Newtonsoft.Json;
using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using Web.App_Start;

namespace WebApiAuthentication.Providers
{
    public class SimpleAuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        public SimpleAuthorizationServerProvider()
        {
            
        }

        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }

        public Model.Usuario Usuario { get; set; }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            var container = StructuremapMvc.StructureMapDependencyScope.Container;

            var usuarioService = container.GetInstance<IUsuarioService>();

            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });            

            var username = context.UserName;
            var password = context.Password;

            this.Usuario = usuarioService.Login(username, password);

            if (this.Usuario == null)
            {
                context.SetError("invalid_grant", "The user name or password is incorrect.");
                return;
            }

            container.Configure(c =>
            {
                c.For<Usuario>().Use(Usuario).Named("usuarioLogado");
            });

            var identity = new ClaimsIdentity(context.Options.AuthenticationType);
                identity.AddClaim(new Claim(ClaimTypes.Email, this.Usuario.Email));
                identity.AddClaim(new Claim(ClaimTypes.Role, this.Usuario.Perfil.Nome.ToUpper()));

            context.Validated(identity);
        }

        public override Task TokenEndpointResponse(OAuthTokenEndpointResponseContext context)
        {
            context.AdditionalResponseParameters.Add("Usuario", this.Usuario.NomeUsuario);
            context.AdditionalResponseParameters.Add("AlterarSenha", this.Usuario.AlterarSenha);
            context.AdditionalResponseParameters.Add("Permissoes", JsonConvert.SerializeObject(this.Usuario.Perfil.Permissoes.Select(x => x.Nome)));

            return base.TokenEndpointResponse(context);
        }
    }
}