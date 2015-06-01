using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Utils.Extensions;

namespace Web.ViewModels
{
    public class MovimentacaoSaidaForm : MovimentacaoForm
    {
        public string CategoriaVaga { get; set; }

        public string Vaga { get; set; }

        public string Cliente { get; set; }

        public int TipoPagamento { get; set; }

        public decimal ValorPago { get; set; }

        public decimal ValorVaga { get; set; }

        public decimal ValorAPagar { get; set; }

        public int HorasReferencia { get; set; }
    }
}