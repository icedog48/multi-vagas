using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Web.ViewModels;

namespace Web.ViewModelsValidation
{
    public class EstacionamentoValidator : AbstractValidator<Estacionamento>
    {
        public EstacionamentoValidator()
        {
            //TODO: Verificar se a CNPJ e a Razao Social ja existem cadastradas para outro estacionamento

            RuleFor(estacionamento => estacionamento.CNPJ).NotEmpty();

            RuleFor(estacionamento => estacionamento.RazaoSocial).NotEmpty();

            //RuleFor(estacionamento => estacionamento.Vagas).NotEqual(0);

            //RuleFor(estacionamento => estacionamento.Telefone).NotEmpty();

            //RuleFor(estacionamento => estacionamento.Email).NotEmpty();

            //RuleFor(estacionamento => estacionamento.Logradouro).NotEmpty();

            //RuleFor(estacionamento => estacionamento.Bairro).NotEmpty();

            //RuleFor(estacionamento => estacionamento.UF).NotEmpty();

            //RuleFor(estacionamento => estacionamento.Cidade).NotEmpty();

            //RuleFor(estacionamento => estacionamento.CEP).NotEmpty();
        }
    }
}