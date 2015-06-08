using Model;
using Service.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface IUsuarioService : IService<Usuario>
    {
        Usuario Login(string email, string senha);

        void RegistrarComSenhaDefault(Usuario usuario);

        Usuario GetByEmail(string email);

        void Remove(Usuario usuario);

        void AlterarSenha(Usuario usuario);

        void Registrar(Usuario usuario);

        void Update(Usuario usuario);

        void ValidateInstance(Usuario usuario);
    }
}
