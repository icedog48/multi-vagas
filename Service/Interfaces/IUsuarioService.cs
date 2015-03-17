using Model;
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

        void Registrar(Usuario usuario);
    }
}
