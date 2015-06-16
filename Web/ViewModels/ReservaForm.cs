using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.ViewModels
{
    public class ReservaForm
    {
        public DateTime Data { get; set; }

        public int CategoriaVaga { get; set; }

        public decimal ValorAPagar { get; set; }

        public string Placa { get; set; }
    }
}