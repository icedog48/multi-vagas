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
    public class PermissaoMap : EntityMap<Permissao>
    {
        public PermissaoMap()
        {
            Map(x => x.Nome);

            HasMany(x => x.Perfis);
        }
    }
}
