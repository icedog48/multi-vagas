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
using System.Linq.Expressions;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Utils.Extensions;

namespace Service
{
    public class MovimentacaoService : MultiVagasCRUDService<Movimentacao>, IMovimentacaoService
    {
        private readonly IFuncionarioService funcionarioService;
        private readonly IRepository<Vaga> vagaRepository;
        private readonly IRepository<TipoPagamento> tipoPagamentoRepository;

        public Usuario UsuarioLogado { get { return usuarioLogado; } set { usuarioLogado = value; } }

        public MovimentacaoService
            (
                MovimentacaoValidator validator, 
                Usuario usuarioLogado,
                IRepository<Movimentacao> repository,     
                IFuncionarioService funcionarioService, 
                IRepository<Vaga> vagaRepository,
                IRepository<TipoPagamento> tipoPagamentoRepository
            )
            : base(repository, validator, usuarioLogado)
        {
            this.funcionarioService = funcionarioService;
            this.vagaRepository = vagaRepository;
            this.tipoPagamentoRepository = tipoPagamentoRepository;
        }

        protected override IQueryable<Movimentacao> GetActiveItems()
        {
            var query = repository.Items.Where(x => x.SituacaoRegistro == SituacaoRegistroEnum.ATIVO);

            if (usuarioLogado.TemPerfil(PerfilEnum.FUNCIONARIO)) 
            {
                var funcionario = funcionarioService.GetFuncionarioByUsuario(usuarioLogado);

                query = query.Where(x => !x.Saida.HasValue && x.Vaga.CategoriaVaga.Estacionamento.Id == funcionario.Estacionamento.Id);
            }
            else if (usuarioLogado.TemPerfil(PerfilEnum.ADMIN))
            {
                query = query.Where(x => x.Vaga.CategoriaVaga.Estacionamento.Usuario.Id == usuarioLogado.Id);
            }

            return query;
        }

        public void RegistrarEntrada()
        {
            var funcionario = funcionarioService.GetFuncionarioByUsuario(usuarioLogado);

            repository.ExecuteTransaction(() =>
            {
                var movimentacao = new Movimentacao();
                    movimentacao.RegistrarEntrada(DateTime.Now, funcionario);

                this.Add(movimentacao);
            });
        }

        public Movimentacao RegistrarEntrada(Movimentacao movimentacao)
        {
            var funcionario = funcionarioService.GetFuncionarioByUsuario(usuarioLogado);

            movimentacao.Vaga = GetVagaById(movimentacao.Vaga.Id);

            repository.ExecuteTransaction(() => 
            {
                movimentacao.RegistrarEntrada(DateTime.Now, funcionario);

                this.Add(movimentacao);
            });

            return movimentacao;
        }

        private Vaga GetVagaById(int vagaId)
        {
            return vagaRepository.Items.Where(x => x.Id == vagaId).FirstOrDefault();
        }

        public void RegistrarSaida(Movimentacao movimentacao)
        {
            var funcionario = funcionarioService.GetFuncionarioByUsuario(usuarioLogado);

            movimentacao.Vaga = GetVagaById(movimentacao.Vaga.Id);

            repository.ExecuteTransaction(() =>
            {
                movimentacao.RegistrarSaida(DateTime.Now, funcionario);

                this.Update(movimentacao);
            });
        }


        public void AtualizarVaga(Movimentacao movimentacao, Vaga novaVaga)
        {
            var vagaAntiga = movimentacao.Vaga;
                vagaAntiga.Disponivel = true;

            vagaRepository.Update(vagaAntiga);

            novaVaga = vagaRepository.Get(novaVaga.Id);
            novaVaga.Disponivel = false;
            vagaRepository.Update(novaVaga);

            movimentacao.Vaga = novaVaga;

            repository.Update(movimentacao);
        }

        public IEnumerable<TipoPagamento> GetTiposPagamento()
        {
            return tipoPagamentoRepository.Items.ToList();
        }

        public IEnumerable<Movimentacao> ListarPorPeriodo(MovimentacaoPorPeriodoFilter filter) 
        {
            return filter.Apply(GetActiveItems()).ToList();
        }
    }
}
