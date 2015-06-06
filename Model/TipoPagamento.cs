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

        public virtual string Descricao { get; set; }
    }
}
