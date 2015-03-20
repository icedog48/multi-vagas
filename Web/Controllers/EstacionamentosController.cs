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
    public class EstacionamentosController : ApiController
    {
        public IEstacionamentoService Service { get; set; }

        public EstacionamentosController(IEstacionamentoService service)
        {
            this.Service = service;
        }

        public IEnumerable<TabelaEstacionamento> Get()
        {
            return Mapper.Map<IEnumerable<TabelaEstacionamento>>(Service.GetAll());
        }

        public Estacionamento Get(int id)
        {

            try
            {
                return this.Service.GetById(id);
            }
            catch (Exception ex)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
        }

        public void Post(Estacionamento estacionamento)
        {
            //var validationResult = Validator.Validate(estacionamento);

            //if (!validationResult.IsValid)
            //{
            //    var response = new HttpResponseMessage(HttpStatusCode.PreconditionFailed)
            //    {
            //        Content = new StringContent(JsonConvert.SerializeObject(validationResult)),
            //        ReasonPhrase = "Falha de validação."

            //    };

            //    throw new HttpResponseException(response);
            //}

            //Repository.Add(Mapper.Map<Model.Estacionamento>(estacionamento));

            Service.Add(estacionamento);
        }

        public void Put(int id, Estacionamento estacionamento)
        {
            //var validationResult = Validator.Validate(estacionamento);

            //if (!validationResult.IsValid)
            //{
            //    var response = new HttpResponseMessage(HttpStatusCode.PreconditionFailed)
            //    {
            //        Content = new StringContent(JsonConvert.SerializeObject(validationResult)),
            //        ReasonPhrase = "Falha de validação."

            //    };

            //    throw new HttpResponseException(response);
            //}

            //Repository.Update(Mapper.Map<Model.Estacionamento>(estacionamento));

            Service.Update(estacionamento);
        }

        public void Delete(int id)
        {
            //var estacionamento = Repository.Get(id);

            //Repository.Remove(Mapper.Map<Model.Estacionamento>(estacionamento));

            Service.Remove(new Estacionamento() { Id = id });
        }

        [Route("api/estacionamentos/filtrar")]
        public IEnumerable<TabelaEstacionamento> Filtrar(EstacionamentoFilter filtro) 
        {
            return Mapper.Map<IEnumerable<TabelaEstacionamento>>(Service.GetByFilter(filtro));
        }
    }
}
