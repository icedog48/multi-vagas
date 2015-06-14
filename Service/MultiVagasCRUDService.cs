using FluentValidation;
using Model;
using Model.Common;
using Service.Common;
using Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public abstract class MultiVagasCRUDService<T> : CRUDLogicalExclusionService<T> where T : LogicalExclusionEntity
    {
        protected Usuario usuarioLogado;

        public Usuario UsuarioLogado
        {
            get
            {
                return usuarioLogado;
            }
            set
            {
                usuarioLogado = value;
            }
        }

        public MultiVagasCRUDService(IRepository<T> repository, AbstractValidator<T> validator, Usuario usuarioLogado):base(repository, validator)
        {
            this.usuarioLogado = usuarioLogado;
        }
    }
}
