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

        public EstacionamentoValidator(IRepository<Estacionamento> repository, IRepository<Usuario> usuarioRepository)
        {
            this.repository = repository;

            RuleFor(estacionamento => estacionamento.CNPJ)
                .NotEmpty()
                .NotNull().WithMessage("O campo CNPJ deve ser preenchido.")
                .Length(14).WithMessage("O campo CNPJ deve ter 14 caracteres numéricos.")
                .Must(HaveUniqueCNPJ).WithMessage("Existe outro estacionamento cadastrado com o CNPJ informado.")
                .Must(HaveUniqueCNPJInactive).WithMessage("Existe outro estacionamento INATIVO cadastrado com o CNPJ informado.");

            RuleFor(estacionamento => estacionamento.RazaoSocial)
                .NotEmpty()
                .NotNull().WithMessage("O campo Razão Social deve ser preenchido.")
                .Must(HaveUniqueRazaoSocial).WithMessage("Existe outro estacionamento cadastrado com a Razão Social informada.")
                .Must(HaveUniqueRazaoSocialInactive).WithMessage("Existe outro estacionamento INATIVO cadastrado com a Razão Social informada.");

            RuleFor(estacionamento => estacionamento.Telefone)
                .NotEmpty()
                .NotNull().WithMessage("O campo Telefone deve ser preenchido.")
                .Length(10, 11).WithMessage("O campo Telefone deve ter entre 10 e 11 números.");

            RuleFor(estacionamento => estacionamento.Logradouro)
               .NotEmpty()
               .NotNull().WithMessage("O campo Logradouro deve ser preenchido.");

            RuleFor(estacionamento => estacionamento.Bairro)
                .NotEmpty()
                .NotNull().WithMessage("O campo Bairro deve ser preenchido.");

            RuleFor(estacionamento => estacionamento.UF)
                .NotEmpty()
                .NotNull().WithMessage("O campo UF deve ser preenchido.")
                .Length(2).WithMessage("O campo UF deve ter 2 caracteres.");

            RuleFor(estacionamento => estacionamento.Cidade)
                .NotEmpty()
                .NotNull().WithMessage("O campo Cidade deve ser preenchido.");

            RuleFor(estacionamento => estacionamento.CEP)
                .NotEmpty()
                .NotNull().WithMessage("O campo CEP deve ser preenchido.")
                .Length(8).WithMessage("O campo CEP deve ter 8 caracteres.");
                
        }

        protected virtual bool HaveUniqueRazaoSocial(Estacionamento estacionamento, string razaoSocial)
        {
            return !repository.Items.Any(x => x.RazaoSocial == razaoSocial && x.Id != estacionamento.Id && x.SituacaoRegistro == Model.Common.SituacaoRegistroEnum.ATIVO);
        }

        protected virtual bool HaveUniqueCNPJ(Estacionamento estacionamento, string cnpj)
        {
            return !repository.Items.Any(x => x.CNPJ == cnpj && x.Id != estacionamento.Id && x.SituacaoRegistro == Model.Common.SituacaoRegistroEnum.ATIVO);
        }

        protected virtual bool HaveUniqueRazaoSocialInactive(Estacionamento estacionamento, string razaoSocial)
        {
            return !repository.Items.Any(x => x.RazaoSocial == razaoSocial && x.Id != estacionamento.Id && x.SituacaoRegistro == Model.Common.SituacaoRegistroEnum.INATIVO);
        }

        protected virtual bool HaveUniqueCNPJInactive(Estacionamento estacionamento, string cnpj)
        {
            return !repository.Items.Any(x => x.CNPJ == cnpj && x.Id != estacionamento.Id && x.SituacaoRegistro == Model.Common.SituacaoRegistroEnum.INATIVO);
        }
    }
}
