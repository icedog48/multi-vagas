using AutoMapper;
using FluentValidation;
using Model;
using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Web.Http;
using Web.App_Start;
using Web.Controllers.Attributes;
using Web.ViewModels;

namespace Web.Controllers
{
    [NHSession]
    [MultivagasAuthorize]
    public class UsuariosController : ApiController
    {
        public IUsuarioService Service { get; set; }

        public UsuariosController(IUsuarioService service)
        {
            this.Service = service;
        }

        [HttpPost]
        [Route("api/usuarios/alterarsenha")]
        public void AlterarSenha (UsuarioForm usuarioForm) 
        {
            try
            {
                var claimsIdentity = (ClaimsIdentity)User.Identity;

                var usuario = Mapper.Map<Usuario>(usuarioForm);
                usuario.Email = claimsIdentity.FindFirst(ClaimTypes.Email).Value;

                Service.AlterarSenha(usuario);
            }
            catch (ValidationException ex)
            {
                ThrowHttpResponseException(ex);
            }
        }

        protected virtual void ThrowHttpResponseException(ValidationException ex)
        {
            var errors = new StringBuilder();

            foreach (var error in ex.Errors) errors.AppendLine(error.ErrorMessage);

            var response = ControllerContext.Request.CreateErrorResponse(HttpStatusCode.BadRequest, errors.ToString());

            throw new HttpResponseException(response);
        }
    }
}
