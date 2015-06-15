using FluentValidation;
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
    public class ClienteService : MultiVagasCRUDService<Cliente>, IClienteService
    {
        private IUsuarioService usuarioService;

        public ClienteService(IRepository<Cliente> repository, ClienteValidator validator, IUsuarioService usuarioService, Usuario usuarioLogado)
            : base(repository, validator, usuarioLogado)
        {
            this.usuarioService = usuarioService;
        }

        protected override IQueryable<Cliente> GetActiveItems()
        {
            var query = repository.Items.Where(x => x.SituacaoRegistro == SituacaoRegistroEnum.ATIVO);

            return query;
        }

        public void Add(Cliente cliente, string senha)
        {
            var usuario = new Usuario()
            {
                AlterarSenha = false,
                Email = cliente.Email,
                NomeUsuario = cliente.Email,
                Perfil = new Perfil(PerfilEnum.USUARIO),
                Senha = senha,
                SituacaoRegistro = SituacaoRegistroEnum.ATIVO
            };

            repository.ExecuteTransaction(() => 
            {
                usuarioService.Registrar(usuario);

                cliente.Usuario = usuario;

                var result = (new ClienteValidator(repository)).Validate(cliente);

                if (!result.IsValid) throw new ValidationException(result.Errors);

                repository.Add(cliente);
            });
        }

        public Cliente GetClienteByUsuario(Usuario usuario)
        {
            return repository.Items.Where(x => x.Usuario.Id == usuario.Id).FirstOrDefault();
        }
    }
}
