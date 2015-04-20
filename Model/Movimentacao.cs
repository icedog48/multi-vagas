using Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Movimentacao : LogicalExclusionEntity
    {
        public virtual string Ticket { get; set; }
                
        public virtual DateTime Entrada { get; set; }
                
        public virtual Vaga Vaga { get; set; }
                
        public virtual Funcionario FuncionarioEntrada { get; set; }
                
        public virtual Cliente Cliente { get; set; }

        public virtual string Placa { get; set; }

        public virtual Funcionario FuncionarioSaida { get; set; }

        public virtual TipoPagamento TipoPagamento { get; set; }

        public virtual DateTime? Saida { get; set; }

        public virtual decimal? ValorPago { get; set; }
    }
}
