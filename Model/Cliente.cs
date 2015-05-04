using Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Cliente : LogicalExclusionEntity
    {
        public virtual string Nome { get; set; }
                   
        public virtual string CPF { get; set; }
                   
        public virtual string Telefone { get; set; }
                   
        public virtual string Email { get; set; }
    }
}
