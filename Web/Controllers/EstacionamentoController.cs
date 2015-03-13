using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Web.ViewModels;
using Storage;
using AutoMapper;
using Web.ViewModelsValidation;
using Newtonsoft.Json;
using Web.Controllers.Attributes;


namespace Web.Controllers
{
    [NHSession]
    [Route("api/estacionamentos")]
    [Authorize]
    public class EstacionamentoController : ApiController
    {
        public IRepository<Model.Estacionamento> Repository { get; set; }

        public EstacionamentoValidator Validator { get; set; }

        public EstacionamentoController(IRepository<Model.Estacionamento> repository, EstacionamentoValidator validator)
        {
            this.Repository = repository;
            this.Validator = validator;
        }

        [Route("api/estacionamentos")]
        public IEnumerable<Estacionamento> Get()
        {
            return Mapper.Map<IEnumerable<ViewModels.Estacionamento>>(Repository.Items.AsEnumerable());
        }

        [Route("api/estacionamentos/{id}")]
        public Estacionamento Get(int id)
        {
            var estacionamentos = from item in Repository.Items
                                  where item.Id.Equals(id)
                                  select item;

            if (!estacionamentos.Any()) throw new HttpResponseException(HttpStatusCode.NotFound);

            return Mapper.Map<ViewModels.Estacionamento>(estacionamentos.First());
        }

        public void Post(Estacionamento estacionamento)
        {
            var validationResult = Validator.Validate(estacionamento);

            if (!validationResult.IsValid)
            {
                var response = new HttpResponseMessage(HttpStatusCode.PreconditionFailed)
                {
                    Content = new StringContent(JsonConvert.SerializeObject(validationResult)),
                    ReasonPhrase = "Falha de validação."

                };

                throw new HttpResponseException(response);
            }

            Repository.Add(Mapper.Map<Model.Estacionamento>(estacionamento));
        }

        [Route("api/estacionamentos/{id}")]
        public void Put(int id, Estacionamento estacionamento)
        {
            var validationResult = Validator.Validate(estacionamento);

            if (!validationResult.IsValid)
            {
                var response = new HttpResponseMessage(HttpStatusCode.PreconditionFailed)
                {
                    Content = new StringContent(JsonConvert.SerializeObject(validationResult)),
                    ReasonPhrase = "Falha de validação."

                };

                throw new HttpResponseException(response);
            }

            Repository.Update(Mapper.Map<Model.Estacionamento>(estacionamento));
        }

        [Route("api/estacionamentos/{id}")]
        public void Delete(int id)
        {
            var estacionamento = Repository.Get(id);

            Repository.Remove(Mapper.Map<Model.Estacionamento>(estacionamento));
        }
    }
}
