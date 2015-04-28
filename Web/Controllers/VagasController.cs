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
    public class VagasController : ApiController
    {
        public ICategoriaVagaService Service { get; set; }

        public VagasController(ICategoriaVagaService service)
        {
            this.Service = service;
        }

        [HttpGet]
        [Route("api/vagas/{id}")]
        public VagaCombo Get(int id)
        {
            try
            {
                return Mapper.Map<VagaCombo>(this.Service.GetVagaById(id));
            }
            catch (Exception ex)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
        }

        [HttpGet]
        [Route("api/vagas/disponiveis/{id}")]
        public IEnumerable<VagaCombo> Disponiveis(int id)
        {
            return Mapper.Map<IEnumerable<VagaCombo>>(Service.VagasDisponiveis(id));
        }

        [HttpGet]
        [Route("api/vagas/categorias")]
        public IEnumerable<CategoriaVagaCombo> Categorias()
        {
            return Mapper.Map<IEnumerable<CategoriaVagaCombo>>(Service.GetAll());
        } 
    }
}
