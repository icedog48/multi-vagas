using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.ViewModels
{
    public class MovimentacaoPorPeriodoTable
    {
        public string Estacionamento { get; set; }

        public string Placa { get; set; }

        public string CategoriaVaga { get; set; }

        public string Vaga { get; set; }

        public string TipoPagamento { get; set; }

        public string Data { get; set; }

        public int HorasReferencia { get; set; }

        public decimal ValorPago { get; set; }
    }
}