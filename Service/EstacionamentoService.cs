using Model;
using Service.Filters;
using Service.Interfaces;
using Storage;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Utils.Extensions;

namespace Service
{
    public class EstacionamentoService : CRUDService<Estacionamento>, IEstacionamentoService
    {

        public EstacionamentoService(IRepository<Estacionamento> repository)
            : base(repository)
        {
            
        }
    }

}
