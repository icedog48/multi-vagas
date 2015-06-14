using Model;
using Service.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Service.Interfaces
{
    public interface IClienteService : IMultiVagasCRUDService<Cliente>
    {

        void Add(Cliente cliente, string senha);

        Cliente GetClienteByUsuario(Usuario usuario);
    }
}
