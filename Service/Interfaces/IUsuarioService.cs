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
        Usuario Login(string usuario, string senha);

        void RegistrarComSenhaDefault(Usuario usuario);

        void Atualizar(Usuario usuario);

        Usuario GetByLogin(string login);

        void Remove(Usuario usuario);
    }
}
