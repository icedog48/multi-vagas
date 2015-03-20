using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Service.Filters
{
    public class AdminFilter : IFilter<Usuario>
    {


        public IQueryable<Usuario> Apply(IQueryable<Usuario> query)
        {
            return query;
        }
    }
}
