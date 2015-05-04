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
    public class MovimentacaoFilter : IFilter<Movimentacao>
    {
        public string Placa { get; set; }

        public string Ticket { get; set; }


        public IQueryable<Movimentacao> Apply(IQueryable<Movimentacao> query)
        {
            if (!string.IsNullOrEmpty(Placa))
                query = query.Where(movimentacao => movimentacao.Placa.Contains(Placa));

            if (!string.IsNullOrEmpty(Ticket))
                query = query.Where(movimentacao => movimentacao.Ticket.Contains(Ticket));

            return query;
        }
    }
}
