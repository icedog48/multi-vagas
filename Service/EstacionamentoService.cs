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
        private IUsuarioService usuarioService;        

        public EstacionamentoService(IRepository<Estacionamento> repository, EstacionamentoValidator validator, IUsuarioService usuarioService, Usuario usuarioLogado)
            : base(repository, validator, usuarioLogado)
        {
            this.usuarioService = usuarioService;
        }    

        protected virtual void RegistrarAdministrador(Usuario obj)
        {
            obj.Perfil = new Perfil(PerfilEnum.ADMIN);
            
            usuarioService.RegistrarComSenhaDefault(obj);
        }

        protected override IQueryable<Estacionamento> GetActiveItems()
        {
            var query = repository.Items.Where(x => x.SituacaoRegistro == (int)SituacaoRegistroEnum.ATIVO);;

            //Usuario ou Administrador podem listar todos os estacionamentos
            var usuarioOUAdministrador = usuarioLogado.TemPerfil(PerfilEnum.EQUIPE_MULTIVAGAS) || usuarioLogado.TemPerfil(PerfilEnum.USUARIO);

            if (usuarioOUAdministrador) return query;

            query = query.Where(estacionamento => estacionamento.Usuario.Id == usuarioLogado.Id);

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
            if (ExcluirUsuarioAntigo(estacionamento, usuarioAntigo)) usuarioService.Remove(usuarioAntigo);            
        }

        protected virtual bool ExcluirUsuarioAntigo(Estacionamento estacionamento, Usuario usuarioAntigo)
        {
            var administraOutrosEstacionamentos = repository.Items.Any(x => x.Usuario.Id == usuarioAntigo.Id);
            
            return !administraOutrosEstacionamentos; // Só posso excluir o usuario caso ele nao administre outros estacionamentos.
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
    }

}
