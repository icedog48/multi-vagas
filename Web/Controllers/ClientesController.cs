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
using FluentValidation;
using System.Text;


namespace Web.Controllers
{
    [NHSession]
    public class ClientesController : MultiVagasApiController<Cliente>
    {
        public IClienteService Service { get; set; }

        public ClientesController(IClienteService service): base(service)
        {
            this.Service = service;
        }

        [HttpPost]
        [Route("api/clientes")]
        public void Registrar(ClienteForm clienteForm)
        {
            try
            {
                var cliente = Mapper.Map<Cliente>(clienteForm);

                this.Service.Add(cliente, clienteForm.Senha);
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
