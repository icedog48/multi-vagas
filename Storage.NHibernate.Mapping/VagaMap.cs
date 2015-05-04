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
    public class VagaMap : EntityMap<Vaga>
    {
        public VagaMap()
        {
            Map(x => x.Codigo);

            Map(x => x.Disponivel);

            References(x => x.CategoriaVaga);
        }
    }
}
