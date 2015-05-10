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


namespace Web.Controllers
{
    [NHSession]    
    public class EstacionamentosController : ApiController
    {
        public IEstacionamentoService Service { get; set; }

        public IUsuarioService UsuarioService { get; set; }

        public string Senha { get; set; }

        private bool UsuarioEquipeMultivagas()
        {
            var usuario = User as ClaimsPrincipal;

            return usuario.Claims.Any(claim => claim.Type.Equals(ClaimTypes.Role) && claim.Value.Equals(PerfilEnum.EQUIPE_MULTIVAGAS.ToString()));
        }

        public EstacionamentosController
            (
                IEstacionamentoService service, 
                IUsuarioService usuarioService
            )
        {
            this.Service = service;
            this.UsuarioService = usuarioService;

            this.Senha = "multivagas";
        }

        public IEnumerable<EstacionamentoTable> Get()
        {
            return Mapper.Map<IEnumerable<EstacionamentoTable>>(Service.GetAll());
        }

        public EstacionamentoForm Get(int id)
        {
            try
            {
                var estacionamento = this.Service.GetById(id);

                if (UsuarioEquipeMultivagas()) return Mapper.Map<EstacionamentoFormAdministrador>(estacionamento);

                return Mapper.Map<EstacionamentoForm>(estacionamento);
            }
            catch (Exception ex)
            {
                var response = ControllerContext.Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);

                throw new HttpResponseException(response);
            }
        }

        [MultivagasAuthorize]
        public void Post(EstacionamentoFormAdministrador estacionamentoForm)
        {
            var estacionamento = Mapper.Map<Estacionamento>(estacionamentoForm);

            //this.VerficaLogin(estacionamentoForm.Usuario.Login);

            Service.Add(estacionamento);
        }

        [MultivagasAuthorize]
        public void Put(int id, EstacionamentoFormAdministrador estacionamentoForm)
        {
            var estacionamento = Mapper.Map<Estacionamento>(estacionamentoForm);

            //this.VerficaLogin(estacionamentoForm.Usuario.Login);

            Service.Update(estacionamento);   
        }

        [MultivagasAuthorize]
        public void Delete(int id)
        {
            Service.Remove(new Estacionamento() { Id = id });
        }

        [Route("api/estacionamentos/filtrar")]
        public IEnumerable<EstacionamentoTable> Filtrar(EstacionamentoFilter filtro) 
        {
            var estacionamentos = Service.GetByFilter(filtro);

            return Mapper.Map<IEnumerable<EstacionamentoTable>>(estacionamentos);
        }

        //private UsuarioFormEstacionamento VerficaLogin(string login)
        //{
        //    try
        //    {
        //        Usuario usuario = Service.VerficaLogin(login);

        //        return Mapper.Map<UsuarioFormEstacionamento>(usuario);                
        //    }
        //    catch (Exception ex)
        //    {
        //        var response = ControllerContext.Request.CreateErrorResponse(HttpStatusCode.Unauthorized, "ADMINISTRADOR_INVALIDO"); //"O login informado não possui perfil de administrador."

        //        throw new HttpResponseException(response);
        //    }
        //}

        [Route("api/estacionamentos/tipospagamento/{estacionamentoId}")]
        public IEnumerable<TipoPagamento> GetTiposPagamento(int estacionamentoId)
        {
            return Service.GetListTipoPagamento(estacionamentoId);
        }
    }
}
