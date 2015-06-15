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
    public class ReservaValidator : AbstractValidator<Reserva>
    {
        private IRepository<Reserva> repository;

        public ReservaValidator(IRepository<Reserva> repository)
        {
            this.repository = repository;

            RuleFor(reserva => reserva.Data)
                .NotEmpty()
                .NotNull().WithMessage("O campo Data deve ser preenchido.")
                .Must(NaoTerReserva).WithMessage("Já existe uma reserva feita para essa data.") ;
        }

        private bool NaoTerReserva(Reserva reserva, DateTime data)
        {
            return !repository.Items.Any(x => x.Data.Date == data.Date && x.Cliente.Id == reserva.Cliente.Id);
        }
    }
}
