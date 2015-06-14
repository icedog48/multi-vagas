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
    public class CategoriaVagaValidator : AbstractValidator<CategoriaVaga>
    {
        private IRepository<CategoriaVaga> repository;

        public CategoriaVagaValidator(IRepository<CategoriaVaga> repository)
        {
            this.repository = repository;

            RuleFor(categoria => categoria.Descricao)
                .NotEmpty()
                .Must(DescricaoNaoRepetida).WithMessage("Já existe uma categoria com a descrição informada.")
                .Must(DescricaoNaoRepetidaInactive).WithMessage("Já existe uma categoria INATIVA com a descrição informada.");

            RuleFor(categoria => categoria.Sigla)
                .NotEmpty()
                .Must(SiglaNaoRepetida).WithMessage("Já existe uma categoria com a sigla informada.")
                .Must(SiglaNaoRepetidaInactive).WithMessage("Já existe uma categoria INATIVA com a sigla informada.");
        }

        private bool DescricaoNaoRepetida(CategoriaVaga categoriaVaga, string descricao)
        {
            return !repository.Items.Any(categoria => categoria.Descricao == descricao && categoria.Id != categoriaVaga.Id && categoria.Estacionamento.Id == categoriaVaga.Estacionamento.Id && categoria.SituacaoRegistro == Model.Common.SituacaoRegistroEnum.ATIVO);
        }

        private bool SiglaNaoRepetida(CategoriaVaga categoriaVaga, string sigla)
        {
            return !repository.Items.Any(categoria => categoria.Sigla == sigla && categoria.Id != categoriaVaga.Id && categoria.Estacionamento.Id == categoriaVaga.Estacionamento.Id && categoria.SituacaoRegistro == Model.Common.SituacaoRegistroEnum.ATIVO);
        }

        private bool DescricaoNaoRepetidaInactive(CategoriaVaga categoriaVaga, string descricao)
        {
            return !repository.Items.Any(categoria => categoria.Descricao == descricao && categoria.Id != categoriaVaga.Id && categoria.Estacionamento.Id == categoriaVaga.Estacionamento.Id && categoria.SituacaoRegistro == Model.Common.SituacaoRegistroEnum.INATIVO);
        }

        private bool SiglaNaoRepetidaInactive(CategoriaVaga categoriaVaga, string sigla)
        {
            return !repository.Items.Any(categoria => categoria.Sigla == sigla && categoria.Id != categoriaVaga.Id && categoria.Estacionamento.Id == categoriaVaga.Estacionamento.Id && categoria.SituacaoRegistro == Model.Common.SituacaoRegistroEnum.INATIVO);
        }
    }
}
