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

        public IQueryable<Movimentacao> Apply(IQueryable<Movimentacao> query)
        {
            return query;
        }
    }
}
