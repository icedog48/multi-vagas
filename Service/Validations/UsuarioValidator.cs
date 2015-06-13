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
    public class UsuarioValidator : AbstractValidator<Usuario>
    {
        private IRepository<Usuario> repository;

        public UsuarioValidator(IRepository<Usuario> repository)
        {
            this.repository = repository;

            RuleFor(usuario => usuario.Email)
                .NotEmpty()
                .NotNull().WithMessage("O campo Email deve ser preenchido.")
                .Must(HaveUniqueEmail).WithMessage("Existe um usuário cadastrado com o Email informado.")
                .Must(HaveUniqueEmailInactive).WithMessage("Existe um usuário INATIVO cadastrado com o Email informado.");

            RuleFor(usuario => usuario.Senha)
                .NotEmpty()
                .NotNull().WithMessage("O campo Senha deve ser preenchido.");

            RuleFor(usuario => usuario.NomeUsuario)
                .NotEmpty()
                .NotNull().WithMessage("O campo Usuário deve ser preenchido.");

            RuleFor(usuario => usuario.Perfil)
                .NotNull().WithMessage("O campo Perfil deve ser preenchido.");
        }

        protected virtual bool HaveUniqueEmailInactive(Usuario usuario, string email)
        {
            return !repository.Items.Any(x => x.Email == email && x.Id != usuario.Id && x.SituacaoRegistro == Model.Common.SituacaoRegistroEnum.INATIVO);
        }

        protected virtual bool HaveUniqueEmail(Usuario usuario, string email)
        {
            return !repository.Items.Any(x => x.Email == email && x.Id != usuario.Id && x.SituacaoRegistro == Model.Common.SituacaoRegistroEnum.ATIVO);
        }
    }
}
