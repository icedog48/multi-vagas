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
    public class EstacionamentoMap : LogicalExclusionEntityMap<Estacionamento>
    {
        public EstacionamentoMap()
        {
            Map(x => x.CEP);
            Map(x => x.Logradouro);
            Map(x => x.Bairro);            
            Map(x => x.Cidade);
            Map(x => x.UF);

            Map(x => x.CNPJ);
            Map(x => x.RazaoSocial);
            Map(x => x.Email);
            Map(x => x.Telefone);

            Map(x => x.ConfirmaSaida);
            Map(x => x.PermiteReserva);

            References(x => x.Usuario);

            HasMany(x => x.CagetoriasVaga);
        }
    }
}
