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
    [MultivagasAuthorize]
    public class PerfisController : ApiController
    {
        public IPerfilService Service { get; set; }

        public PerfisController(IPerfilService service)
        {
            this.Service = service;
        }

        public IEnumerable<PerfilCombo> Get()
        {
            return Mapper.Map<IEnumerable<PerfilCombo>>(Service.GetAll());
        }

        [Route("api/perfis/perfisfuncionario")]
        public IEnumerable<PerfilCombo> GetPerfisFuncionario()
        {
            return Mapper.Map<IEnumerable<PerfilCombo>>(Service.GetPerfisFuncionario());
        }
    }
}
