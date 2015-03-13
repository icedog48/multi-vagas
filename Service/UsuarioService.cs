using Model;
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

        public UsuarioService(IRepository<Usuario> repository)
        {
            this.repository = repository;
        }

        public Usuario Login(string usuario, string senha)
        {
            senha = Encryption.Encrypt(senha);

            var query = repository.Items.Where(x => x.Login == usuario && x.Senha == senha);

            if (query.Any()) return query.First();

            return null;
        }

        public void Registrar(Usuario usuario) 
        {
            usuario.Senha = Encryption.Encrypt(usuario.Senha);

            repository.Add(usuario);
        }
    }
}
