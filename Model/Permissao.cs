using Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class Permissao : Entity
    {
        public virtual string Nome { get; set; }

        public virtual IList<Perfil> Perfis { get; set; }

        public override string ToString()
        {
            return this.Nome;
        }
    }
}
