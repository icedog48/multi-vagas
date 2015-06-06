using Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Reserva : Entity
    {
        public virtual Vaga Vaga { get; set; }

        public virtual DateTime Data { get; set; }

        public virtual Cliente Cliente { get; set; }
    }
}
