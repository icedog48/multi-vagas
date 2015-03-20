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
    [Authorize]
    public class VagasController : ApiController
    {
        public ICategoriaVagaService Service { get; set; }

        public VagasController(ICategoriaVagaService service)
        {
            this.Service = service;
        }

        public IEnumerable<TabelaVaga> Get()
        {
            return Mapper.Map<IEnumerable<TabelaVaga>>(Service.GetAll());
        }

        public FormularioVaga Get(int id)
        {
            try
            {
                return Mapper.Map<FormularioVaga>(this.Service.GetById(id));
            }
            catch (Exception ex)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
        }

        public void Post(FormularioVaga vaga)
        {
            Service.Add(Mapper.Map<CategoriaVaga>(vaga), vaga.Quantidade);
        }

        public void Put(int id, FormularioVaga vaga)
        {
            Service.Update(Mapper.Map<CategoriaVaga>(vaga));
        }

        public void Delete(int id)
        {
            Service.Remove(new CategoriaVaga() { Id = id });
        }

        [Route("api/vagas/filtrar")]
        public IEnumerable<TabelaVaga> Filtrar(CategoriaVagaFilter filtro) 
        {
            return Mapper.Map<IEnumerable<TabelaVaga>>(Service.GetByFilter(filtro));
        }
    }
}
