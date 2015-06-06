using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.ViewModels
{
    public class MovimentacaoEntradaForm : MovimentacaoForm
    {
        public int CategoriaVaga { get; set; }

        public int Vaga { get; set; }

        public int? Cliente { get; set; }
    }
}
