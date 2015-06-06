using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.ViewModels
{
    public class EstacionamentoForm
    {
        public int Id { get; set; }

        public string CNPJ { get; set; }

        public string RazaoSocial { get; set; }

        public string Telefone { get; set; }

        public string Email { get; set; }

        public string Logradouro { get; set; }

        public string Bairro { get; set; }

        public string UF { get; set; }

        public string Cidade { get; set; }

        public string CEP { get; set; }

        public bool ConfirmaSaida { get; set; }

        public bool PermiteReserva { get; set; }        
    }
}