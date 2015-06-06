using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.ViewModels
{
    public class MovimentacaoForm
    {
        public int Id { get; set; }

        public string Ticket { get; set; }

        public string Placa { get; set; }

        public string Entrada { get; set; }
    }
}
