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
    public class UsuarioMap : LogicalExclusionEntityMap<Usuario>
    {
        public UsuarioMap()
        {
            Map(x => x.Email);
            Map(x => x.Login);
            Map(x => x.Senha);   

            References(x => x.Perfil);
        }
    }
}
