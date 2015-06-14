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

            RuleFor(movimentacao => movimentacao.Placa)
                .NotEmpty()
                .NotNull().WithMessage("O campo placa deve ser preenchido.")
                .Must(NaoRegistrarEntradaSemSaida).WithMessage("Já foi registrada uma entrada para a placa '{0}'", x => x.Placa);
        }

        private bool NaoRegistrarEntradaSemSaida(Movimentacao movimentacao, string placa)
        {
            return string.IsNullOrEmpty(placa) || !repository.Items.Any(x => x.Placa == placa && x.Id != movimentacao.Id && x.Saida == null);
        }
    }
}
