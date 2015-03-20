using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.ViewModels
{
    public class TabelaEstacionamento
    {
        public int Id { get; set; }

        public string CNPJ { get; set; }

        public string RazaoSocial { get; set; }
        
        public string Telefone { get; set; }

        public string Endereco { get; set; }
    }
}