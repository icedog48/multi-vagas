using Model;
using Model.Common;
using Service.Common;
using Service.Filters;
using Service.Interfaces;
using Service.Validations;
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
    public class EstacionamentoService : CRUDLogicalExclusionService<Estacionamento>, IEstacionamentoService
    {

        public EstacionamentoService(IRepository<Estacionamento> repository, EstacionamentoValidator validator)
            : base(repository, validator)
        {
            
        }

        protected override IQueryable<Estacionamento> GetActiveItems()
        {
            return repository.Items.Where(x => x.SituacaoRegistro == (int)SituacaoRegistroEnum.ATIVO);
        }
    }

}
