using Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Funcionario : LogicalExclusionEntity
    {
        public virtual Usuario Usuario { get; set; }

        public virtual Estacionamento Estacionamento { get; set; }

        public virtual string Matricula { get; set; }

        public virtual string Nome { get; set; }

        public virtual string Telefone { get; set; }

        public virtual string CPF { get; set; }

        public virtual string Logradouro { get; set; }

        public virtual string Bairro { get; set; }

        public virtual string UF { get; set; }

        public virtual string Cidade { get; set; }

        public virtual string CEP { get; set; }

        public virtual TimeSpan HoraInicio { get; set; }

        public virtual TimeSpan HoraSaida { get; set; }

        public virtual DateTime DataAdmissao { get; set; }

        public virtual decimal Salario { get; set; }

        public virtual string Obs { get; set; }
    }
}
