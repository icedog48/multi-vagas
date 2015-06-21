using Model;
using Model.Common;
using Service.Common.Filters;
using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Filters
{
    public class EstacionamentoFilter : IFilter<Estacionamento>
    {
        public string CNPJ { get; set; }

        public string RazaoSocial { get; set; }

        public string Logradouro { get; set; }

        public string UF { get; set; }

        public string Cidade { get; set; }

        public bool? PermiteReserva { get; set; }

        public IQueryable<Estacionamento> Apply(IQueryable<Estacionamento> query)
        {
            if (!string.IsNullOrEmpty(CNPJ))
                query = query.Where(estacionamento => estacionamento.CNPJ.Contains(CNPJ));

            if (!string.IsNullOrEmpty(RazaoSocial))
                query = query.Where(estacionamento => estacionamento.RazaoSocial.Contains(RazaoSocial));

            if (!string.IsNullOrEmpty(UF))
                query = query.Where(estacionamento => estacionamento.UF.Contains(UF));

            if (!string.IsNullOrEmpty(Cidade))
                query = query.Where(estacionamento => estacionamento.Cidade.Contains(Cidade));

            if (this.PermiteReserva.HasValue)
            {
                //query = query.Where(estacionamento => estacionamento.CagetoriasVaga.Any(categoria => categoria.SituacaoRegistro == SituacaoRegistroEnum.ATIVO && categoria.Vagas.Any(vaga=> vaga.Disponivel )));

                query = query.Where(estacionamento => estacionamento.CagetoriasVaga.Any(categoria => categoria.SituacaoRegistro == SituacaoRegistroEnum.ATIVO));

                query = query.Where(estacionamento => estacionamento.PermiteReserva == this.PermiteReserva.Value);
            }

            return query;
        }
    }
}
