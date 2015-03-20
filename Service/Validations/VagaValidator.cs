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
    public class VagaValidator : AbstractValidator<Vaga>
    {
        private IRepository<Vaga> repository;

        public VagaValidator(IRepository<Vaga> repository)
        {
            this.repository = repository;
        }
    }
}
