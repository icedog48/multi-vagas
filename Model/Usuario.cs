using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public enum PerfilEnum
    {
        EQUIPE_MULTIVAGAS = 1,
        ADMIN = 2
    }

    public class Usuario : Entity
    {
        public virtual string Email { get; set; }

        public virtual string Senha { get; set; }

        public virtual string Login { get; set; }

        public virtual Perfil Perfil { get; set; }
    }
}
