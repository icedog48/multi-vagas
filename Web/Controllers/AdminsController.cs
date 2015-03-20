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
    public class AdminsController : ApiController
    {
        public IAdminService Service { get; set; }

        public AdminsController(IAdminService service)
        {
            this.Service = service;
        }

        public IEnumerable<TabelaAdmin> Get()
        {
            return Mapper.Map<IEnumerable<TabelaAdmin>>(Service.GetAll());
        }

        public FormularioAdmin Get(int id)
        {
            try
            {
                var admin = this.Service.GetById(id);
                    admin.Senha = string.Empty;

                return Mapper.Map<FormularioAdmin>(admin);
            }
            catch (Exception ex)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
        }

        public void Post(FormularioAdmin admin)
        {
            Service.Add(Mapper.Map<Usuario>(admin));
        }

        public void Put(int id, FormularioAdmin admin)
        {
            Service.Update(Mapper.Map<Usuario>(admin));
        }

        public void Delete(int id)
        {
            Service.Remove(new Usuario() { Id = id });
        }

        [Route("api/admins/filtrar")]
        public IEnumerable<TabelaAdmin> Filtrar(AdminFilter filtro) 
        {
            return Mapper.Map<IEnumerable<TabelaAdmin>>(Service.GetByFilter(filtro));
        }
    }
}
