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
    public class MovimentacaoPorCategoriaFilter : IFilter<Movimentacao>
    {
        public int? Estacionamento { get; set; }

        public int? Categoria { get; set; }

        public IQueryable<Movimentacao> Apply(IQueryable<Movimentacao> query)
        {
            if (Estacionamento.HasValue)
            {
                query = query.Where(x => x.FuncionarioEntrada.Estacionamento.Id == Estacionamento.Value);
            }

            if (Categoria.HasValue)
            {
                query = query.Where(x => x.Vaga.CategoriaVaga.Id == Categoria.Value);
            }

            return query;
        }
    }
}
