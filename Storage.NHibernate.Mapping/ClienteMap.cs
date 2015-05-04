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
    public class ClienteMap : LogicalExclusionEntityMap<Cliente>
    {
        public ClienteMap()
        {
            Map(x => x.Nome);
            Map(x => x.CPF);
            Map(x => x.Email);            
            Map(x => x.Telefone);
        }
    }
}
