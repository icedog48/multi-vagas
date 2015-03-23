using Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Vaga : Entity
    {
        public virtual CategoriaVaga CategoriaVaga { get; set; }

        public virtual string Codigo { get; set; }

        public virtual bool Disponivel { get; set; }
    }
}
