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

            RuleFor(funcionario => funcionario.Matricula)
                .NotEmpty()
                .NotNull().WithMessage("O campo Matrícula deve ser preenchido.")
                .Length(10).WithMessage("O campo Matrícula deve ter 10 caracteres numéricos.");

            RuleFor(funcionario => funcionario.Nome)
                .NotEmpty()
                .NotNull().WithMessage("O campo Nome deve ser preenchido.");

            RuleFor(funcionario => funcionario.Telefone)
                .NotEmpty()
                .NotNull().WithMessage("O campo Telefone deve ser preenchido.")
                .Length(10, 11).WithMessage("O campo Telefone deve ter entre 10 e 11 números.");

            RuleFor(funcionario => funcionario.CPF)
                .NotEmpty()
                .NotNull().WithMessage("O campo CPF deve ser preenchido.")
                .Must(HaveUniqueCPF).WithMessage("Existe outro funcionario cadastrado com o cpf informado.");

            RuleFor(funcionario => funcionario.Logradouro)
                .NotEmpty()
                .NotNull().WithMessage("O campo Logradouro deve ser preenchido.");

            RuleFor(funcionario => funcionario.Bairro)
                .NotEmpty()
                .NotNull().WithMessage("O campo Bairro deve ser preenchido.");

            RuleFor(funcionario => funcionario.UF)
                .NotEmpty()
                .NotNull().WithMessage("O campo UF deve ser preenchido.")
                .Length(2, 3).WithMessage("O campo UF deve ter entre 2 e 3 caracteres.");

            RuleFor(funcionario => funcionario.Cidade)
                .NotEmpty()
                .NotNull().WithMessage("O campo Cidade deve ser preenchido.");

            RuleFor(funcionario => funcionario.CEP)
                .NotEmpty()
                .NotNull().WithMessage("O campo CEP deve ser preenchido.")
                .Length(8).WithMessage("O campo CEP deve ter 8 caracteres.");

            RuleFor(funcionario => funcionario.DataAdmissao)
                .NotNull()
                .GreaterThan(DateTime.MinValue).WithMessage("O campo Data da Admissão deve ser preenchido.");

            RuleFor(funcionario => funcionario.Salario)
                .GreaterThan(0).WithMessage("O campo Salário deve ser preenchido.");
        }

        protected virtual bool HaveUniqueCPF(Funcionario funcionario, string cpf)
        {
            return !repository.Items.Any(x => x.CPF == cpf && x.Id != funcionario.Id);
        }
    }
}
