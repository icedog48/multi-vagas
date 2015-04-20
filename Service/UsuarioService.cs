using Model;
using Model.Common;
using Service.Interfaces;
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

        public virtual string SenhaDefault { get; set; }

        public UsuarioService(IRepository<Usuario> repository)
        {
            this.repository = repository;
            this.SenhaDefault = "multivagas";
        }

        public Usuario Login(string usuario, string senha)
        {
            senha = Encryption.Encrypt(senha);

            var query = repository.Items.Where(x => x.Login == usuario && x.Senha == senha);

            if (query.Any()) return query.First();

            return null;
        }

        public void RegistrarComSenhaDefault(Usuario usuario) 
        {
            usuario.Senha = Encryption.Encrypt(SenhaDefault);

            usuario.SituacaoRegistro = (int)SituacaoRegistroEnum.ATIVO;

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

        public void Atualizar(Usuario usuario)
        {
            usuario.Senha = Encryption.Encrypt(usuario.Senha);

            repository.Update(usuario);
        }

        public Usuario GetByLogin(string login)
        {
            var query = repository.Items.Where(usuario => usuario.Login == login);

            if (query.Count() <= 0) return null;

            return query.First();
        }

        public void Remove(Usuario usuario)
        {
            repository.Remove(usuario);
        }
    }
}
