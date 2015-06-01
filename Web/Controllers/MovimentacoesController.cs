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
        [Route("api/movimentacoes/registrarentrada")]
        public void RegistrarEntrada(MovimentacaoEntradaForm movimentacao)
        {
            Service.RegistrarEntrada(Mapper.Map<Movimentacao>(movimentacao));
        }

        [HttpGet]
        [Route("api/movimentacoes/prepararsaida/{movimentacao}")]
        public MovimentacaoSaidaForm PrepararSaida(int movimentacao)
        {
            try
            {
                return Mapper.Map<MovimentacaoSaidaForm>(this.Service.GetById(movimentacao));
            }
            catch (Exception ex)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
        }

        [HttpPut]
        [Route("api/movimentacoes/registrarsaida/{id}")]
        public void RegistrarSaida(int id, MovimentacaoSaidaForm movimentacaoSaida)
        {
            var movimentacao = Service.GetById(id);
                movimentacao.TipoPagamento = Mapper.Map<TipoPagamento>(movimentacaoSaida.TipoPagamento);
                movimentacao.ValorPago = movimentacaoSaida.ValorPago;    

            Service.RegistrarSaida(movimentacao);
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

        [HttpPost]
        [Route("api/movimentacoes/atualizarvaga")]
        public void AtualizarVaga(MovimentacaoEntradaForm entrada)
        {
            var movimentacao = Service.GetById(entrada.Id);

            Service.AtualizarVaga(movimentacao, Mapper.Map<Vaga>(entrada.Vaga));
        }

        [HttpGet]
        [Route("api/movimentacoes/tipospagamento")]
        public IEnumerable<TipoPagamentoCombo> ListarTiposPagamento()
        {
            return Mapper.Map<IEnumerable<TipoPagamentoCombo>>(Service.GetTiposPagamento());
        }
    }
}
