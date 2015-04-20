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
        private IRepository<Funcionario> funcionarioRepository;

        public MovimentacaoService(IRepository<Movimentacao> repository, MovimentacaoValidator validator, Usuario usuarioLogado, IRepository<Funcionario> funcionarioRepository)
            : base(repository, validator, usuarioLogado)
        {
            this.funcionarioRepository = funcionarioRepository;
        }

        protected override IQueryable<Movimentacao> GetActiveItems()
        {
            var ativo = (int)SituacaoRegistroEnum.ATIVO;

            var query = repository.Items.Where(x => x.SituacaoRegistro == ativo);

            return query;
        }

        public void RegistrarEntrada(Movimentacao movimentacao)
        {
            var funcionario = funcionarioRepository.Items.Where(x => x.Usuario.Id == usuarioLogado.Id).FirstOrDefault();

            movimentacao.SituacaoRegistro = (int)SituacaoRegistroEnum.ATIVO;
            movimentacao.FuncionarioEntrada = funcionario;
            movimentacao.Entrada = DateTime.Now;
            movimentacao.Ticket = DateTime.Now.ToString("yyyyMMddHHmmss");

            repository.Add(movimentacao);
        }

        public void RegistrarSaida(Movimentacao movimentacao)
        {
            throw new NotImplementedException();
        }
    }
}
