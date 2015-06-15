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
    public interface IMovimentacaoService : IMultiVagasCRUDService<Movimentacao>
    {
        Movimentacao RegistrarEntrada(Movimentacao movimentacao);

        void RegistrarSaida(Movimentacao movimentacao);

        void AtualizarVaga(Movimentacao movimentacao, Vaga vaga);

        IEnumerable<TipoPagamento> GetTiposPagamento();

        IEnumerable<Movimentacao> ListarPorPeriodo(MovimentacaoPorPeriodoFilter filter);
    }
}
