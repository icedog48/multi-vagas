using Model;
using Model.Common;
using Service.Common;
using Service.Filters;
using Service.Interfaces;
using Service.Validations;
using Storage;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Utils.Extensions;

namespace Service
{
    public class CategoriaVagaService : CRUDLogicalExclusionService<CategoriaVaga>, ICategoriaVagaService
    {
        private IRepository<Vaga> vagaRepository;
        private CategoriaVagaValidator categoriaVagaValidator;

        public CategoriaVagaService
            (
                IRepository<CategoriaVaga> repository, 
                IRepository<Vaga> vagaRepository,
                CategoriaVagaValidator categoriaVagaValidator
            )
            : base(repository, categoriaVagaValidator)
        {
            this.vagaRepository = vagaRepository;
            this.categoriaVagaValidator = categoriaVagaValidator;
        }

        protected virtual Vaga NovaVaga(int indice, CategoriaVaga categoria) 
        {
            var codigo = categoria.Sigla;

            if (indice < 10)
            {
                codigo += "0";
            }

            codigo += indice;
            
            return new Vaga()
            {
                Codigo = codigo,
                CategoriaVaga = categoria,
                Disponivel = true
            };
        }

        public virtual void Add(CategoriaVaga categoria, int vagas) 
        {
            this.Add(categoria);

            List<Vaga> listaVagas = new List<Vaga>(vagas);

            for (int indice = 0; indice < vagas; indice++) vagaRepository.Add(NovaVaga(indice, categoria));
        }

        protected override IQueryable<CategoriaVaga> GetActiveItems()
        {
            var ativo = (int)SituacaoRegistroEnum.ATIVO;

            return repository.Items.Where(x => x.Estacionamento.SituacaoRegistro == ativo && x.SituacaoRegistro == ativo);
        }
    }

}
