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

        public IEnumerable<VagaTable> Get()
        {
            return Mapper.Map<IEnumerable<VagaTable>>(Service.GetAll());
        }

        public VagaForm Get(int id)
        {
            try
            {
                return Mapper.Map<VagaForm>(this.Service.GetById(id));
            }
            catch (Exception ex)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
        }

        public void Post(VagaForm vaga)
        {
            Service.Add(Mapper.Map<CategoriaVaga>(vaga), vaga.Quantidade);
        }

        public void Put(int id, VagaForm vaga)
        {
            Service.Update(Mapper.Map<CategoriaVaga>(vaga));
        }

        public void Delete(int id)
        {
            Service.Remove(new CategoriaVaga() { Id = id });
        }

        [Route("api/vagas/filtrar")]
        public IEnumerable<VagaTable> Filtrar(CategoriaVagaFilter filtro) 
        {
            return Mapper.Map<IEnumerable<VagaTable>>(Service.GetByFilter(filtro));
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
