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
    public class EstacionamentoValidator : AbstractValidator<Estacionamento>
    {
        private IRepository<Estacionamento> repository;

        public EstacionamentoValidator(IRepository<Estacionamento> repository)
        {
            this.repository = repository;
        }
    }
}
