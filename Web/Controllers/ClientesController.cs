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


namespace Web.Controllers
{
    [NHSession]
    public class ClientesController : ApiController
    {
        public IClienteService Service { get; set; }

        public ClientesController(IClienteService service)
        {
            this.Service = service;
        }

        [HttpPost]
        [Route("api/clientes")]
        public void Registrar(ClienteForm clienteForm)
        {
            var cliente = Mapper.Map<Cliente>(clienteForm);

            this.Service.Add(cliente, clienteForm.Senha);
        }
    }
}
