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
    public class FuncionarioValidator : AbstractValidator<Funcionario>
    {
        private IRepository<Funcionario> repository;

        public FuncionarioValidator(IRepository<Funcionario> repository)
        {
            this.repository = repository;
        }
    }
}
