using Model;
using Service.Common.Interfaces;
using Service.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface IEstacionamentoService : ICRUDService<Estacionamento>
    {

        Usuario UsuarioLogado
        {
            get;
            set;
        }

        Usuario VerficaLogin(string login);

        IEnumerable<TipoPagamento> GetListTipoPagamento(int estacionamentoId);
    }
}
