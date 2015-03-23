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
        public IQueryable<Funcionario> Apply(IQueryable<Funcionario> query)
        {
            return query;
        }
    }
}
