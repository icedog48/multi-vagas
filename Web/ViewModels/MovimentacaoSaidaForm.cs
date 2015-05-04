using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.ViewModels
{
    public class MovimentacaoSaidaForm
    {
        public int Id { get; set; }

        public int TipoPagamento { get; set; }

        public decimal ValorPago { get; set; }
    }
}