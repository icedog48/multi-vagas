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
    public class CategoriaVagaFilter : IFilter<CategoriaVaga>
    {
        public int? Estacionamento { get; set; }

        public string Descricao { get; set; }

        public IQueryable<CategoriaVaga> Apply(IQueryable<CategoriaVaga> query)
        {
            if (Estacionamento.HasValue)
                query = query.Where(vaga => vaga.Estacionamento.Id == Estacionamento.Value);

            if (!string.IsNullOrEmpty(Descricao))
                query = query.Where(vaga => vaga.Descricao.Contains(Descricao));

            return query;
        }
    }
}
