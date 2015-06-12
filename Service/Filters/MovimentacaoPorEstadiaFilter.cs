using Model;
using Service.Common.Filters;
using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Filters
{
    public class MovimentacaoPorEstadiaFilter : IFilter<Movimentacao>
    {
        public int? Estacionamento { get; set; }

        public string HorasReferenciaInicio { get; set; }

        public string HorasReferenciaFim { get; set; }

        public IQueryable<Movimentacao> Apply(IQueryable<Movimentacao> query)
        {
            if (Estacionamento.HasValue)
            {
                query = query.Where(x => x.FuncionarioEntrada.Estacionamento.Id == Estacionamento.Value);
            }
            
            if (!string.IsNullOrEmpty(HorasReferenciaInicio))
            {
                var horaReferencia = Convert.ToDateTime(HorasReferenciaInicio).TimeOfDay.TotalHours;

                query = query.Where(x => x.Estadia >= horaReferencia);
            }

            if (!string.IsNullOrEmpty(HorasReferenciaFim))
            {
                var horaReferencia = Convert.ToDateTime(HorasReferenciaFim).TimeOfDay.TotalHours;

                query = query.Where(x => x.Estadia <= horaReferencia);
            }

            return query;
        }
    }
}
