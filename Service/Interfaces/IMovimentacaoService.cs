using Model;
using Service.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface IMovimentacaoService : ICRUDService<Movimentacao>
    {
        void RegistrarEntrada(Movimentacao movimentacao);

        void RegistrarSaida(Movimentacao movimentacao);

        void AtualizarVaga(Movimentacao movimentacao, Vaga vaga);
    }
}
