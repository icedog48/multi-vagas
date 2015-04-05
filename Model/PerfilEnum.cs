using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils.Extensions;

namespace Model
{
    public enum PerfilEnum
    {
        [Description("Equipe Multivagas")]
        EQUIPE_MULTIVAGAS = 1,
        
        [Description("Administrador")]
        ADMIN = 2,
        
        [Description("Funcionario")]
        FUNCIONARIO,

        [Description("Usuário")]
        USUARIO
    }
}
