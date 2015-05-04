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
    public class CategoriaVagaService : MultiVagasCRUDService<CategoriaVaga>, ICategoriaVagaService
    {
        private IRepository<Vaga> vagaRepository;
        private IFuncionarioService funcionarioService;
        private CategoriaVagaValidator categoriaVagaValidator;

        public CategoriaVagaService
            (
                IRepository<CategoriaVaga> repository, 
                IRepository<Vaga> vagaRepository,
                IFuncionarioService funcionarioService,
                CategoriaVagaValidator categoriaVagaValidator,
                Usuario usuarioLogado
            )
            : base(repository, categoriaVagaValidator, usuarioLogado)
        {
            this.vagaRepository = vagaRepository;
            this.funcionarioService = funcionarioService;
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
            var ativo = SituacaoRegistroEnum.ATIVO;

            var query = repository.Items.Where(x => x.Estacionamento.SituacaoRegistro == ativo && x.SituacaoRegistro == ativo);

            if (usuarioLogado.TemPerfil(PerfilEnum.ADMIN))
            {
                query = query.Where(categoriaVaga => categoriaVaga.Estacionamento.Usuario.Id == usuarioLogado.Id);
            }

            if (usuarioLogado.TemPerfil(PerfilEnum.FUNCIONARIO))
            {
                var funcionario = funcionarioService.GetFuncionarioByUsuario(usuarioLogado);

                query = query.Where(categoriaVaga => categoriaVaga.Estacionamento.Id == funcionario.Estacionamento.Id);
            }

            return query;
        }

        public override void Update(CategoriaVaga categoriaVaga)
        {
            categoriaVaga.Vagas = vagaRepository.Items.Where(vaga => vaga.CategoriaVaga.Id == categoriaVaga.Id).ToList();

            base.Update(categoriaVaga);
        }


        public IEnumerable<Vaga> VagasDisponiveis(int categoriaId)
        {
            return vagaRepository.Items.Where(vaga => vaga.Disponivel && vaga.CategoriaVaga.Id == categoriaId);
        }


        public Vaga GetVagaById(int id)
        {
            return vagaRepository.Items.Where(vaga => vaga.Id == id).FirstOrDefault();
        }
    }

}
