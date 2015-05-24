using Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Usuario : LogicalExclusionEntity
    {
        public virtual string Email { get; set; }

        public virtual string Senha { get; set; }

        public virtual string Login { get; set; }

        public virtual Perfil Perfil { get; set; }

        public virtual bool TemPerfil(PerfilEnum perfilEnum)
        {
            return this.Perfil.Id == (int)perfilEnum;
        }

        public virtual bool AlterarSenha { get; set; }
    }
}
