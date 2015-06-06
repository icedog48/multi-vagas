using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Web.ViewModels
{
    public class FuncionarioForm
    {
        public FuncionarioForm()
        {

        }

        public int Id { get; set; }

        public int Estacionamento { get; set; }

        public string Matricula { get; set; }

        public string Nome { get; set; }

        public string CPF { get; set; }

        public string Telefone { get; set; }


        public string Logradouro { get; set; }

        public string Bairro { get; set; }

        public string UF { get; set; }

        public string Cidade { get; set; }

        public string CEP { get; set; }


        public string HoraInicio { get; set; }

        public string HoraSaida { get; set; }

        public DateTime DataAdmissao { get; set; }

        public decimal Salario { get; set; }

        public string Obs { get; set; }

        public UsuarioFormFuncionario Usuario { get; set; }      
    }
}
