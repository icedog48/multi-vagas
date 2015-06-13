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
using System.Text;
using FluentValidation;


namespace Web.Controllers
{
    [NHSession]
    [MultivagasAuthorize]
    public class FuncionariosController : ApiController
    {
        public IFuncionarioService Service { get; set; }

        public FuncionariosController(IFuncionarioService service)
        {
            this.Service = service;
        }

        public IEnumerable<FuncionatioTable> Get()
        {
            return Mapper.Map<IEnumerable<FuncionatioTable>>(Service.GetAll());
        }

        public FuncionarioForm Get(int id)
        {
            try
            {
                var admin = this.Service.GetById(id);

                return Mapper.Map<FuncionarioForm>(admin);
            }
            catch (Exception ex)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
        }

        public void Post(FuncionarioForm funcionarioForm)
        {
            try
            {
                Service.Add(Mapper.Map<Funcionario>(funcionarioForm));
            }
            catch (ValidationException ex)
            {
                ThrowHttpResponseException(ex);
            }
            
        }

        public void Put(int id, FuncionarioForm admin)
        {
            try
            {
                Service.Update(Mapper.Map<Funcionario>(admin));
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

        public void Delete(int id)
        {
            Service.Remove(new Funcionario() { Id = id });
        }

        [Route("api/funcionario/filtrar")]
        public IEnumerable<FuncionatioTable> Filtrar(FuncionarioFilter filtro) 
        {
            return Mapper.Map<IEnumerable<FuncionatioTable>>(Service.GetByFilter(filtro));
        }
    }
}
