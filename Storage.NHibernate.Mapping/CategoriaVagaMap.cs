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
    public class CategoriaVagaMap : LogicalExclusionEntityMap<CategoriaVaga>
    {
        public CategoriaVagaMap()
        {
            Map(x => x.Descricao);

            Map(x => x.Sigla);

            Map(x => x.ValorHora);

            References(x => x.Estacionamento);

            HasMany(x => x.Vagas);
        }
    }
}
