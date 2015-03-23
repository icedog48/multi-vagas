using Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class CategoriaVaga : LogicalExclusionEntity
    {
        public virtual Estacionamento Estacionamento { get; set; }

        public virtual string Sigla { get; set; }

        public virtual string Descricao { get; set; }       

        public virtual decimal ValorHora { get; set; }

        public virtual IEnumerable<Vaga> Vagas { get; set; }
    }
}
