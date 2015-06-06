using Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.ViewModels
{
    public class ClienteForm
    {
        public int Id { get; set; }

        public virtual string Nome { get; set; }
                   
        public virtual string CPF { get; set; }
                   
        public virtual string Telefone { get; set; }
                   
        public virtual string Email { get; set; }

        public virtual string Senha { get; set; }

        public virtual string ConfirmacaoSenha { get; set; }
    }
}
