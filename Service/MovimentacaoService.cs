﻿using Model;
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

            if (usuarioLogado.TemPerfil(PerfilEnum.FUNCIONARIO)) query = query.Where(x => !x.Saida.HasValue);

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
