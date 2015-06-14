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
using Web.App_Start;
using FluentValidation;
using System.Text;


namespace Web.Controllers
{
    [NHSession]
    [MultivagasAuthorize]
    public class VagasController : MultiVagasApiController<CategoriaVaga>
    {
        public ICategoriaVagaService CategoriaVagaService { get; set; }

        public IClienteService ClienteService { get; set; }

        public Usuario UsuarioLogado { get; set; }

        public VagasController
            (
                ICategoriaVagaService categoriaVagaService
            ) : base(categoriaVagaService)
        {
            this.CategoriaVagaService = categoriaVagaService;
        }

        [HttpGet]
        [Route("api/vagas/{id}")]
        public VagaCombo Get(int id)
        {
            try
            {
                return Mapper.Map<VagaCombo>(this.CategoriaVagaService.GetVagaById(id));
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
            return Mapper.Map<IEnumerable<VagaCombo>>(CategoriaVagaService.VagasDisponiveis(id));
        }

        [HttpGet]
        [Route("api/vagas/categorias")]
        public IEnumerable<CategoriaVagaCombo> Categorias()
        {
            return Mapper.Map<IEnumerable<CategoriaVagaCombo>>(CategoriaVagaService.GetAll());
        }

        [HttpGet]
        [Route("api/vagas/categorias/{id}")]
        public IEnumerable<CategoriaVagaCombo> Categorias(int id)
        {
            return Mapper.Map<IEnumerable<CategoriaVagaCombo>>(CategoriaVagaService.GetByEstacionamento(new Estacionamento() { Id = id }));
        } 

        [HttpPost]
        [Route("api/vagas/reservar")]
        public void ReservarVaga(ReservaForm reservaForm)
        {
            var vaga = CategoriaVagaService.VagasDisponiveis(reservaForm.CategoriaVaga).FirstOrDefault();

            var reserva = new Reserva() 
            {
                Data = reservaForm.Data,
                Vaga = vaga
            };

            try
            {
                CategoriaVagaService.ReservarVaga(reserva);
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
