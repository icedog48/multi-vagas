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
    public class ReservaMap : EntityMap<Reserva>
    {
        public ReservaMap()
        {
            Map(x => x.Data);

            References(x => x.Vaga);

            References(x => x.Cliente);
        }
    }
}
