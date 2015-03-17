using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Service.Filters
{
    public interface IFilter<T>
    {
        IQueryable<T> Apply(IQueryable<T> query);
    }
}
