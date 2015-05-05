using Model;
using Model.Common;
using Service.Common;
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
        private IFuncionarioService funcionarioService;
        private IRepository<Vaga> vagaRepository;

        public MovimentacaoService(IRepository<Movimentacao> repository, MovimentacaoValidator validator, Usuario usuarioLogado, IFuncionarioService funcionarioService, IRepository<Vaga> vagaRepository)
            : base(repository, validator, usuarioLogado)
        {
            this.funcionarioService = funcionarioService;
            this.vagaRepository = vagaRepository;
        }

        protected override IQueryable<Movimentacao> GetActiveItems()
        {
            var query = repository.Items.Where(x => x.SituacaoRegistro == SituacaoRegistroEnum.ATIVO);

            return query;
        }

        public void RegistrarEntrada()
        {
            var funcionario = funcionarioService.GetFuncionarioByUsuario(usuarioLogado);

            var movimentacao = new Movimentacao();
                movimentacao.RegistrarEntrada(DateTime.Now, funcionario);

            this.Add(movimentacao);
        }

        public void RegistrarEntrada(Movimentacao movimentacao)
        {
            var funcionario = funcionarioService.GetFuncionarioByUsuario(usuarioLogado);

            movimentacao.Vaga = GetVagaById(movimentacao.Vaga.Id);

            movimentacao.RegistrarEntrada(DateTime.Now, funcionario);

            this.Add(movimentacao);
        }

        private Vaga GetVagaById(int vagaId)
        {
            return vagaRepository.Items.Where(x => x.Id == vagaId).FirstOrDefault();
        }

        public void RegistrarSaida(Movimentacao movimentacao)
        {
            var funcionario = funcionarioService.GetFuncionarioByUsuario(usuarioLogado);

            movimentacao.Vaga = GetVagaById(movimentacao.Vaga.Id);

            movimentacao.RegistrarSaida(DateTime.Now, funcionario);

            this.Update(movimentacao);
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
    }
}
