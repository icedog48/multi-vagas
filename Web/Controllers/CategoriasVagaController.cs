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
    public class CategoriasVagaController : ApiController
    {
        public ICategoriaVagaService Service { get; set; }

        public CategoriasVagaController(ICategoriaVagaService service)
        {
            this.Service = service;
        }

        public IEnumerable<CategoriaVagaTable> Get()
        {
            return Mapper.Map<IEnumerable<CategoriaVagaTable>>(Service.GetAll());
        }

        public CategoriaVagaForm Get(int id)
        {
            try
            {
                return Mapper.Map<CategoriaVagaForm>(this.Service.GetById(id));
            }
            catch (Exception ex)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
        }


        public void Post(CategoriaVagaForm vaga)
        {
            try
            {
                Service.Add(Mapper.Map<CategoriaVaga>(vaga), vaga.Quantidade);
            }
            catch (ValidationException ex)
            {
                ThrowHttpResponseException(ex);
            }
        }

        public void Put(int id, CategoriaVagaForm vaga)
        {
            try
            {
                Service.Update(Mapper.Map<CategoriaVaga>(vaga));
            }
            catch (ValidationException ex)
            {
                ThrowHttpResponseException(ex);
            }
        }

        public void Delete(int id)
        {
            Service.Remove(new CategoriaVaga() { Id = id });
        }

        [HttpPost]
        [Route("api/categoriasvaga/filtrar")]
        public IEnumerable<CategoriaVagaTable> Filtrar(CategoriaVagaFilter filtro) 
        {
            return Mapper.Map<IEnumerable<CategoriaVagaTable>>(Service.GetByFilter(filtro));
        }

        private void ThrowHttpResponseException(ValidationException ex)
        {
            var errors = new StringBuilder();

            foreach (var error in ex.Errors) errors.AppendLine(error.ErrorMessage);

            var response = ControllerContext.Request.CreateErrorResponse(HttpStatusCode.InternalServerError, errors.ToString());

            throw new HttpResponseException(response);
        }
    }
}
