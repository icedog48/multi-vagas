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
    [MultivagasAuthorize]
    public class MovimentacoesController : ApiController
    {
        public IMovimentacaoService Service { get; set; }

        public MovimentacoesController(IMovimentacaoService service)
        {
            this.Service = service;
        }

        public IEnumerable<MovimentacaoPorPeriodoTable> Get()
        {
            return Mapper.Map<IEnumerable<MovimentacaoPorPeriodoTable>>(Service.GetAll());
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
        [Route("api/movimentacoes/periodo")]
        public IEnumerable<MovimentacaoPorPeriodoTable> Get(MovimentacaoPorPeriodoFilter filter)
        {
            var movimentacoes = Service.GetByFilter(filter);

            return Mapper.Map<IEnumerable<MovimentacaoPorPeriodoTable>>(movimentacoes);
        }

        [HttpPost]
        [Route("api/movimentacoes/categoria")]
        public IEnumerable<MovimentacaoPorCategoriaTable> Get(MovimentacaoPorCategoriaFilter filter)
        {
            var movimentacoes = Service.GetByFilter(filter);

            return Mapper.Map<IEnumerable<MovimentacaoPorCategoriaTable>>(movimentacoes);
        }

        [HttpPost]
        [Route("api/movimentacoes/estadia")]
        public IEnumerable<MovimentacaoPorEstadiaTable> Get(MovimentacaoPorEstadiaFilter filter)
        {
            var movimentacoes = Service.GetByFilter(filter);

            return Mapper.Map<IEnumerable<MovimentacaoPorEstadiaTable>>(movimentacoes);
        }

        [HttpPost]
        [Route("api/movimentacoes/registrarentrada")]
        public void RegistrarEntrada(MovimentacaoEntradaForm movimentacao)
        {
            try
            {
                Service.RegistrarEntrada(Mapper.Map<Movimentacao>(movimentacao));
            }
            catch (ValidationException ex)
            {
                ThrowHttpResponseException(ex);
            }
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
            try
            {
                var movimentacao = Service.GetById(id);
                    movimentacao.TipoPagamento = Mapper.Map<TipoPagamento>(movimentacaoSaida.TipoPagamento);
                    movimentacao.ValorPago = movimentacaoSaida.ValorPago;

                Service.RegistrarSaida(movimentacao);
            }
            catch (ValidationException ex)
            {
                ThrowHttpResponseException(ex);
            }
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

        protected virtual void ThrowHttpResponseException(ValidationException ex)
        {
            var errors = new StringBuilder();

            foreach (var error in ex.Errors) errors.AppendLine(error.ErrorMessage);

            var response = ControllerContext.Request.CreateErrorResponse(HttpStatusCode.BadRequest, errors.ToString());

            throw new HttpResponseException(response);
        }
    }
}
