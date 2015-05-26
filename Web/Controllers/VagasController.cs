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


namespace Web.Controllers
{
    [NHSession]
    [MultivagasAuthorize]
    public class VagasController : ApiController
    {
        public ICategoriaVagaService CategoriaVagaService { get; set; }

        public IClienteService ClienteService { get; set; }

        public Usuario UsuarioLogado { get; set; }

        public VagasController
            (
                ICategoriaVagaService categoriaVagaService
            )
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

            CategoriaVagaService.ReservarVaga(reserva);
        } 

    }
}
