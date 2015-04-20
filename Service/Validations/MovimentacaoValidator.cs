using FluentValidation;
using FluentValidation.Validators;
using Model;
using Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Validations
{
    public class MovimentacaoValidator : AbstractValidator<Movimentacao>
    {
        private IRepository<Movimentacao> repository;

        public MovimentacaoValidator(IRepository<Movimentacao> repository)
        {
            this.repository = repository;
        }
    }
}
