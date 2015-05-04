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
    public class EstacionamentoService : MultiVagasCRUDService<Estacionamento>, IEstacionamentoService
    {
        private readonly IUsuarioService usuarioService;
        private readonly IFuncionarioService funcionarioService;
        private readonly IRepository<TipoPagamento> tipoPagamentoRepository;

        public EstacionamentoService
            (
                IRepository<Estacionamento> repository, 
                EstacionamentoValidator validator, 
                IUsuarioService usuarioService, 
                IFuncionarioService funcionarioService, 
                Usuario usuarioLogado,
                IRepository<TipoPagamento> tipoPagamentoRepository
            )
            : base(repository, validator, usuarioLogado)
        {
            this.usuarioService = usuarioService;
            this.funcionarioService = funcionarioService;
            this.tipoPagamentoRepository = tipoPagamentoRepository;
        }    

        protected virtual void RegistrarAdministrador(Usuario obj)
        {
            obj.Perfil = new Perfil(PerfilEnum.ADMIN);
            
            usuarioService.RegistrarComSenhaDefault(obj);
        }

        protected override IQueryable<Estacionamento> GetActiveItems()
        {
            var query = repository.Items.Where(x => x.SituacaoRegistro == SituacaoRegistroEnum.ATIVO);            

            if (usuarioLogado.TemPerfil(PerfilEnum.ADMIN))
            {
                query = query.Where(estacionamento => estacionamento.Usuario.Id == usuarioLogado.Id);
            }
            else if (usuarioLogado.TemPerfil(PerfilEnum.FUNCIONARIO))
            {
                var funcionario = funcionarioService.GetFuncionarioByUsuario(usuarioLogado);

                query = query.Where(estacionamento => estacionamento.Id == funcionario.Estacionamento.Id);
            }

            return query;
        }

        public override void Add(Estacionamento obj)
        {
            RegistrarAdministrador(obj.Usuario);

            base.Add(obj);
        }

        public override void Update(Estacionamento estacionamento)
        {
            var usuarioAntigo = ObterAdministradorAntigo(estacionamento.Id);

            if (estacionamento.Usuario == null) // Recupera o usuario antigo, caso venha da tela sem o usuario preenchido
            {
                estacionamento.Usuario = usuarioAntigo;
            }
            else if (estacionamento.Usuario != null && estacionamento.Usuario.IsNew())
            {
                RegistrarAdministrador(estacionamento.Usuario);
            }

            base.Update(estacionamento);

            //Caso o usuário tenha sido alterado, remove o antigo
            if (PossoExcluirUsuarioAntigo(estacionamento, usuarioAntigo)) usuarioService.Remove(usuarioAntigo);            
        }

        protected virtual bool PossoExcluirUsuarioAntigo(Estacionamento estacionamento, Usuario usuarioAntigo)
        {
            var administraOutrosEstacionamentos = repository.Items.Any(x => x.Usuario.Id == usuarioAntigo.Id);

            // Só posso excluir o usuario caso ele nao administre outros estacionamentos.
            return !administraOutrosEstacionamentos && usuarioAntigo.TemPerfil(PerfilEnum.ADMIN); 
        }

        protected virtual Usuario ObterAdministradorAntigo(int estacionamentoId)
        {
            return this.GetById(estacionamentoId).Usuario;
        }
        
        public virtual Usuario VerficaLogin(string login)
        {
            var usuario = usuarioService.GetByLogin(login);

            if (usuario != null && !usuario.TemPerfil(PerfilEnum.ADMIN)) throw new UnauthorizedAccessException("O login informado não possui perfil de administrador.");

            return usuario;
        }

        public IEnumerable<TipoPagamento> GetListTipoPagamento(int estacionamentoId)
        {
            return tipoPagamentoRepository.Items.Where(x => x.Estacionamento.Id == estacionamentoId);
        }
    }

}
