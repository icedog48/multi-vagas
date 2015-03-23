﻿using Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Estacionamento : LogicalExclusionEntity
    {
        public virtual string CNPJ { get; set; }

        public virtual string RazaoSocial { get; set; }

        public virtual int Vagas { get; set; }

        public virtual string Telefone { get; set; }

        public virtual string Email { get; set; }

        public virtual string Logradouro { get; set; }

        public virtual string Bairro { get; set; }

        public virtual string UF { get; set; }

        public virtual string Cidade { get; set; }

        public virtual string CEP { get; set; }

        public virtual bool ConfirmaSaida { get; set; }

        public virtual bool PermiteReserva { get; set; }

        public virtual string EnderecoFormatado()
        {
            return this.Logradouro + ", " + this.Bairro + ", " + this.Cidade + ", " + this.UF;
        }
    }
}
