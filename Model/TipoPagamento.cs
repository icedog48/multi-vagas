using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model.Common;

namespace Model
{
    public class TipoPagamento : Entity
    {
        public TipoPagamento()
        {

        }

        public TipoPagamento(TipoPagamentoEnum tipoPagamento) : this()
        {
            this.Id = (int)tipoPagamento;
        }

        public virtual string Descricao { get; set; }
    }
}
