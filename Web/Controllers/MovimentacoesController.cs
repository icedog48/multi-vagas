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
    public class MovimentacoesController : ApiController
    {
        public IMovimentacaoService Service { get; set; }

        public MovimentacoesController(IMovimentacaoService service)
        {
            this.Service = service;
        }

        public IEnumerable<MovimentacaoTable> Get()
        {
            return Mapper.Map<IEnumerable<MovimentacaoTable>>(Service.GetAll());
        }

        [HttpGet]
        [Route("api/movimentacoes/{id}")]
        public MovimentacaoEntradaForm Get(int id)
        {
            try
            {
                return Mapper.Map<MovimentacaoEntradaForm>(this.Service.GetById(id));
            }
            catch (Exception ex)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
        }

        [HttpPost]
        public void RegistrarEntrada(MovimentacaoEntradaForm vaga)
        {
            Service.RegistrarEntrada(Mapper.Map<Movimentacao>(vaga));
        }

        [HttpPut]
        public void RegistrarSaida(int id, MovimentacaoSaidaForm vaga)
        {
            Service.RegistrarSaida(Mapper.Map<Movimentacao>(vaga));
        }

        public void Delete(int id)
        {
            Service.Remove(new Movimentacao() { Id = id });
        }

        [Route("api/movimentacoes/filtrar")]
        public IEnumerable<MovimentacaoTable> Filtrar(MovimentacaoFilter filtro) 
        {
            return Mapper.Map<IEnumerable<MovimentacaoTable>>(Service.GetByFilter(filtro));
        }

        [HttpPut]
        [Route("api/movimentacoes/{id}")]
        public void AtualizarVaga(int id, int vagaId)
        {
            var movimentacao = Service.GetById(id);                

            Service.AtualizarVaga(movimentacao, Mapper.Map<Vaga>(vagaId));
        }
    }
}
