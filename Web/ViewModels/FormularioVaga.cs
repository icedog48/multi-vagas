﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.ViewModels
{
    public class FormularioVaga
    {
        public int Estacionamento { get; set; }

        public string Descricao { get; set; }

        public string Sigla { get; set; }

        public int Quantidade { get; set; }

        public decimal ValorHora { get; set; }
    }
}