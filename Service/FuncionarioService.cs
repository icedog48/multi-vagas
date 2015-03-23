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
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Utils.Extensions;

namespace Service
{
    public class FuncionarioService : CRUDLogicalExclusionService<Funcionario>, IFuncionarioService
    {
        private IUsuarioService usuarioService;

        public FuncionarioService(IRepository<Funcionario> repository, FuncionarioValidator validator, IUsuarioService usuarioService)
            : base(repository, validator)
        {
            this.usuarioService = usuarioService;
        }

        public override void Add(Funcionario obj)
        {
            obj.Usuario.Login = obj.Matricula;
            obj.Usuario.Senha = obj.Matricula;

            usuarioService.Registrar(obj.Usuario);

            base.Add(obj);
        }

        public override void Update(Funcionario obj)
        {
            obj.Usuario = usuarioService.GetById(obj.Usuario.Id);

            usuarioService.Atualizar(obj.Usuario);

            base.Update(obj);
        }

        protected override IQueryable<Funcionario> GetActiveItems()
        {
            var ativo = (int)SituacaoRegistroEnum.ATIVO;

            return repository.Items.Where(x => x.Estacionamento.SituacaoRegistro == ativo && x.SituacaoRegistro == ativo);
        }
    }
}
