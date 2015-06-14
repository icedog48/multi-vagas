using Model;
using Model.Common;
using Service.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface IMultiVagasCRUDService<T> : ICRUDService<T> where T : Entity
    {
        Usuario UsuarioLogado
        {
            get;
            set;
        }
    }
}
