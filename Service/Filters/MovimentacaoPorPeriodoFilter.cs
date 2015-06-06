using Model;
using Service.Common.Filters;
using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Filters
{
    public class MovimentacaoPorPeriodoFilter : IFilter<Movimentacao>
    {
        public int? Estacionamento { get; set; }

        public DateTime DataInicial { get; set; }

        public DateTime DataFinal { get; set; }

        public IQueryable<Movimentacao> Apply(IQueryable<Movimentacao> query)
        {
            if (Estacionamento.HasValue)
            {
                query = query.Where(x => x.FuncionarioEntrada.Estacionamento.Id == Estacionamento.Value);
            }

            if (DataInicial > DateTime.MinValue && DataInicial < DateTime.MaxValue)
            {
                query = query.Where(x => x.Entrada.Date >= DataInicial.Date);
            }

            if (DataFinal > DateTime.MinValue && DataFinal < DateTime.MaxValue)
            {
                query = query.Where(x => x.Saida.HasValue && x.Saida.Value.Date <= DataFinal.Date);
            }

            return query;
        }
    }
}
