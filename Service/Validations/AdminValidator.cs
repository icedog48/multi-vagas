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
    public class AdminValidator : AbstractValidator<Usuario>
    {
        private IRepository<Usuario> repository;

        public AdminValidator(IRepository<Usuario> repository)
        {
            this.repository = repository;
        }
    }
}
