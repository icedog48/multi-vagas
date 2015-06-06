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
    public class ClienteValidator : AbstractValidator<Cliente>
    {
        private IRepository<Cliente> repository;

        public ClienteValidator(IRepository<Cliente> repository)
        {
            this.repository = repository;
        }
    }
}
