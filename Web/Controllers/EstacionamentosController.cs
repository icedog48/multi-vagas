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
using System.Security.Claims;
using FluentValidation;
using System.Text;
using System.Web;
using Web.App_Start;


namespace Web.Controllers
{
    [NHSession]    
    public class EstacionamentosController : ApiController
    {
        public IEstacionamentoService Service { get; set; }

        private bool UsuarioEquipeMultivagas()
        {
            var usuario = User as ClaimsPrincipal;

            return usuario.Claims.Any(claim => claim.Type.Equals(ClaimTypes.Role) && claim.Value.Equals(PerfilEnum.EQUIPE_MULTIVAGAS.ToString()));
        }

        public EstacionamentosController
            (
                IEstacionamentoService service
            )
        {
            this.Service = service;
        }

        public IEnumerable<EstacionamentoTable> Get()
        {
            var usuario = User as ClaimsPrincipal;

            return Mapper.Map<IEnumerable<EstacionamentoTable>>(Service.GetAll());
        }

        public EstacionamentoForm Get(int id)
        {
            try
            {
                var estacionamento = this.Service.GetById(id);

                if (UsuarioEquipeMultivagas()) return Mapper.Map<EstacionamentoForm>(estacionamento);

                return Mapper.Map<EstacionamentoForm>(estacionamento);
            }
            catch (Exception ex)
            {
                var response = ControllerContext.Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);

                throw new HttpResponseException(response);
            }
        }

        [MultivagasAuthorize]
        public void Post(EstacionamentoForm estacionamentoForm)
        {
            try
            {
                var estacionamento = Mapper.Map<Estacionamento>(estacionamentoForm);

                Service.Add(estacionamento);
            }
            catch (ValidationException ex)
            {
                ThrowHttpResponseValidationException(ex);
            } 
        }

        [MultivagasAuthorize]
        public void Put(int id, EstacionamentoForm estacionamentoForm)
        {
            try
            {
                var estacionamento = Mapper.Map<Estacionamento>(estacionamentoForm);

                Service.Update(estacionamento); 
            }
            catch (ValidationException ex)
            {
                ThrowHttpResponseValidationException(ex);
            }  
        }

        [MultivagasAuthorize]
        public void Delete(int id)
        {
            Service.Remove(new Estacionamento() { Id = id });
        }

        [Route("api/estacionamentos/filtrar")]
        public IEnumerable<EstacionamentoTable> Filtrar(EstacionamentoFilter filtro) 
        {
            if (!HttpContext.Current.User.Identity.IsAuthenticated)
            {
                var container = StructuremapMvc.StructureMapDependencyScope.Container;

                var usuario = new Usuario() 
                {
                };

                //Coloca uma instancia do objeto Usuario disponivel para ser injetado nos serviços            
                container.Configure(c =>
                { 
                    c.For<Usuario>().Use(usuario).Named("usuarioLogado");
                });
            }
            
            var estacionamentos = Service.GetByFilter(filtro);

            return Mapper.Map<IEnumerable<EstacionamentoTable>>(estacionamentos);
        }

        [Route("api/estacionamentos/tipospagamento/{estacionamentoId}")]
        public IEnumerable<TipoPagamento> GetTiposPagamento(int estacionamentoId)
        {
            return Service.GetListTipoPagamento(estacionamentoId);
        }

        protected virtual void ThrowHttpResponseValidationException(ValidationException ex)
        {
            var errors = new StringBuilder();

            foreach (var error in ex.Errors) errors.AppendLine(error.ErrorMessage);

            var response = ControllerContext.Request.CreateErrorResponse(HttpStatusCode.BadRequest, errors.ToString());

            throw new HttpResponseException(response);
        }
    }
}
