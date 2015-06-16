using Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Movimentacao : LogicalExclusionEntity
    {

        public virtual string Ticket { get; set; }
                
        public virtual DateTime Entrada { get; set; }
                
        public virtual Vaga Vaga { get; set; }
                
        public virtual Funcionario FuncionarioEntrada { get; set; }
                
        public virtual Cliente Cliente { get; set; }

        public virtual string Placa { get; set; }

        public virtual Funcionario FuncionarioSaida { get; set; }

        public virtual TipoPagamento TipoPagamento { get; set; }

        public virtual DateTime? Saida { get; set; }

        public virtual decimal? ValorPago { get; set; }

        public static string EmitirTicketAcesso(DateTime entrada) 
        {
            return entrada.ToString("yyyyMMddHHmmss");
        }

        public virtual void RegistrarEntrada(DateTime entrada, Funcionario funcionario) 
        {
            this.FuncionarioEntrada = funcionario;

            this.Entrada = entrada;
            this.Ticket = EmitirTicketAcesso(entrada);

            if (this.Vaga != null)
                this.Vaga.Disponivel = false;            
        }

        public virtual void RegistrarSaida(DateTime saida, Funcionario funcionario)
        {
            this.FuncionarioSaida = funcionario;
            this.Saida = saida;
            this.estadia = this.HorasReferencia;
            this.Vaga.Disponivel = true;
        }

        public virtual int HorasReferencia 
        {
            get
            {
                var tempo = (this.Saida.HasValue) ? this.Saida.Value - this.Entrada : DateTime.Now - this.Entrada;

                var resto = tempo.TotalHours - Convert.ToInt32(tempo.TotalHours);

                var horas = Convert.ToInt32(tempo.TotalHours);

                if (resto > 0) horas += 1;

                return horas;
            }
        }

        public virtual decimal ValorAPagar 
        {
            get
            {
                return Math.Round(this.HorasReferencia * this.Vaga.CategoriaVaga.ValorHora, 2);
            }
        }

        private int? estadia;

        public virtual int? Estadia { get { return estadia; } }
    }
}
