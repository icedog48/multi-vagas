﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model.Common;

namespace Model
{
    public class Perfil : Entity
    {
        public virtual string Nome { get; set; }

        public virtual IList<Permissao> Permissoes { get; set; }
    }
}
