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
    public class FuncionarioService : MultiVagasCRUDService<Funcionario>, IFuncionarioService
    {
        private IUsuarioService usuarioService;

        public FuncionarioService(IRepository<Funcionario> repository, FuncionarioValidator validator, IUsuarioService usuarioService, Usuario usuarioLogado)
            : base(repository, validator, usuarioLogado)
        {
            this.usuarioService = usuarioService;
        }

        public override void Add(Funcionario obj)
        {
            obj.Matricula = DateTime.Now.ToString("MMddHHmmss");
            obj.Usuario.NomeUsuario = obj.Matricula;
            obj.Usuario.Perfil = new Perfil(PerfilEnum.FUNCIONARIO);

            usuarioService.RegistrarComSenhaDefault(obj.Usuario);

            base.Add(obj);
        }

        public override void Update(Funcionario obj)
        {
            var usuario = usuarioService.GetById(obj.Usuario.Id);
                usuario.NomeUsuario = obj.Matricula;
                usuario.Email = obj.Usuario.Email;

            obj.Usuario = usuario;

            usuarioService.ValidateInstance(obj.Usuario);

            base.Update(obj);
        }

        protected override IQueryable<Funcionario> GetActiveItems()
        {
            var ativo = SituacaoRegistroEnum.ATIVO;

            //Obtem todos os funcionarios onde o funcionario E etacionamento esta com situacao de registro ATIVO
            var query = repository.Items.Where(x => x.SituacaoRegistro == ativo && x.Estacionamento.SituacaoRegistro == ativo);

            if (usuarioLogado.TemPerfil(PerfilEnum.EQUIPE_MULTIVAGAS)) return query;

            //Traz soh os funcionarios que trabalhem em estacionamentos administrados pelo usuario logado
            query = query.Where(funcionario => funcionario.Estacionamento.Usuario.Id == usuarioLogado.Id);

            return query;
        }

        public Funcionario GetFuncionarioByUsuario(Usuario usuario)
        {
            return repository.Items.Where(x => x.Usuario.Id == usuario.Id).FirstOrDefault();
        }
    }
}
