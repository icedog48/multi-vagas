using FluentNHibernate.Mapping;
using Model;
using Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Storage.Nhibernate.Mapping
{
    public class TipoPagamentoMap : EntityMap<TipoPagamento>
    {
        public TipoPagamentoMap()
        {
            Map(x => x.Descricao);
        }
    }
}
