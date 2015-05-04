using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.ViewModels
{
    public class VagaTable
    {
        public int Id { get; set; }

        public string Estacionamento { get; set; }

        public string Categoria { get; set; }

        public decimal ValorHora { get; set; }

        public int Vagas { get; set; }
    }
}