using FluentValidation;
using Model;
using Model.Common;
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
    public class UsuarioService : IUsuarioService
    {
        private IRepository<Usuario> repository;
        private UsuarioValidator validator;

        public virtual string SenhaDefault { get; set; }

        public UsuarioService(IRepository<Usuario> repository, UsuarioValidator validator)
        {
            this.repository = repository;
            this.validator = validator;
            this.SenhaDefault = "multivagas";
        }

        public Usuario Login(string email, string senha)
        {
            senha = Encryption.Encrypt(senha);

            var query = repository.Items.Where(x => x.Email == email && x.Senha == senha);

            if (query.Any()) return query.First();

            return null;
        }

        public void RegistrarComSenhaDefault(Usuario usuario) 
        {
            usuario.Senha = Encryption.Encrypt(SenhaDefault);
            usuario.AlterarSenha = true;

            ValidateInstance(usuario);

            repository.Add(usuario);
        }

        public IEnumerable<Usuario> GetAll()
        {
            return repository.Items.ToList();
        }

        public Usuario GetById(int id)
        {
            var list = from item in repository.Items
                       where item.Id.Equals(id)
                       select item;

            if (!list.Any()) throw new InvalidOperationException();

            return list.First();
        }

        public Usuario GetByEmail(string email)
        {
            var query = repository.Items.Where(usuario => usuario.Email == email);

            if (query.Count() <= 0) return null;

            return query.First();
        }

        public void Remove(Usuario usuario)
        {
            repository.Remove(usuario);
        }

        public void AlterarSenha(Usuario usuario)
        {
            var senha = Encryption.Encrypt(usuario.Senha);

            usuario = this.GetByEmail(usuario.Email);
            usuario.Senha = senha;
            usuario.AlterarSenha = false;

            repository.Update(usuario);
        }
        
        public void Registrar(Usuario usuario)
        {
            usuario.Senha = Encryption.Encrypt(usuario.Senha);

            ValidateInstance(usuario);

            repository.Add(usuario);
        }
        
        public void Update(Usuario usuario)
        {
            ValidateInstance(usuario);

            repository.Update(usuario);
        }

        public void ValidateInstance(Usuario usuario)
        {
            var result = validator.Validate(usuario);

            if (!result.IsValid) throw new ValidationException(result.Errors);
        }
    }
}
