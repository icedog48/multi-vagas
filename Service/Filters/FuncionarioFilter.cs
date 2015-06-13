using Model;
using Service.Common.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Service.Filters
{
    public class FuncionarioFilter : IFilter<Funcionario>
    {
        public int? Estacionamento { get; set; }

        public string Matricula { get; set; }

        public string Nome { get; set; }

        public IQueryable<Funcionario> Apply(IQueryable<Funcionario> query)
        {
            if (Estacionamento.HasValue)
            {
                query = query.Where(x => x.Estacionamento.Id == this.Estacionamento.Value);
            }

            if (!string.IsNullOrEmpty(this.Matricula))
            {
                query = query.Where(x => x.Matricula.Contains(this.Matricula));
            }

            if (!string.IsNullOrEmpty(this.Nome))
            {
                query = query.Where(x => x.Nome.Contains(this.Nome));
            }

            return query;
        }
    }
}
