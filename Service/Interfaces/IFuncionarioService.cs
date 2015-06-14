using Model;
using Service.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Service.Interfaces
{
    public interface IFuncionarioService : IMultiVagasCRUDService<Funcionario>
    {

        Funcionario GetFuncionarioByUsuario(Usuario usuario);
    }
}
