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
    public class MovimentacaoMap : LogicalExclusionEntityMap<Movimentacao>
    {
        public MovimentacaoMap()
        {
            Map(x => x.Entrada);
            Map(x => x.Saida);
            Map(x => x.Placa);
            Map(x => x.Ticket);
            Map(x => x.ValorPago);

            References(x => x.Cliente);
            References(x => x.TipoPagamento);
            References(x => x.Vaga);
            References(x => x.FuncionarioEntrada).Column("Funcionario_Entrada_Id");
            References(x => x.FuncionarioSaida).Column("Funcionario_Saida_Id");
        }
    }
}
