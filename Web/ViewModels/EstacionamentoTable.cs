using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.ViewModels
{
    public class EstacionamentoTable
    {
        public int Id { get; set; }

        public string CNPJ { get; set; }

        public string RazaoSocial { get; set; }
        
        public string Telefone { get; set; }

        public string Endereco { get; set; }
    }
}