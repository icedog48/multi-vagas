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

            RuleFor(cliente => cliente.CPF)
                .NotEmpty()
                .NotNull().WithMessage("O campo CPF deve ser preenchido.")
                .Length(11).WithMessage("O campo CPF deve ter 11 números.")
                .Must(HaveUniqueCPF).WithMessage("Existe outro cliente cadastrado com o cpf informado.");

            RuleFor(cliente => cliente.Telefone)
               .NotEmpty()
               .NotNull().WithMessage("O campo Telefone deve ser preenchido.")
               .Length(10, 11).WithMessage("O campo Telefone deve ter entre 10 e 11 caracteres.");

            RuleFor(cliente => cliente.Nome)
               .NotEmpty()
               .NotNull().WithMessage("O campo Nome deve ser preenchido.");
        }

        protected virtual bool HaveUniqueCPF(Cliente cliente, string cpf)
        {
            return !repository.Items.Any(x => x.CPF == cpf && x.Id != cliente.Id);
        }
    }
}
