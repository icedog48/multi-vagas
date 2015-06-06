using AutoMapper;
using Model;
using StructureMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tests.Unity.Helpers;
using Web.App_Start;
using Web.ViewModels;
using Xunit;

namespace Tests.Unity
{
    
    public class AutomapperTest
    {
        private IContainer container;

        public AutomapperTest()
        {
            //this.container = IoCHelper.Initialize();
        }

        [Fact(DisplayName="Movimentação Entrada - Automapper")]
        public void DeveMapearEntidadeMovimentacaoEntrada() 
        {
            Automapper.Setup();

            var movimentacaoForm = new MovimentacaoEntradaForm()
            {
                Cliente = null,
                CategoriaVaga = 1,
                Vaga = 1,
                Placa = "1234",
                Entrada = DateTime.Now.ToString(),
                Ticket = "123456"
            };

            var movimentacao = Mapper.Map<Movimentacao>(movimentacaoForm);
                movimentacao.Vaga.CategoriaVaga = new CategoriaVaga() { Id = 1 };

            movimentacaoForm = Mapper.Map<MovimentacaoEntradaForm>(movimentacao);

        }

        [Fact(DisplayName = "Movimentação Saida - Automapper")]
        public void DeveMapearEntidadeMovimentacaoSaida()
        {
            Automapper.Setup();

            var movimentacaoForm = new MovimentacaoSaidaForm()
            {
                Cliente = null,
                CategoriaVaga = "CategoriaVaga",
                Vaga = "Vaga",
                Placa = "1234",
                ValorPago = 1,
                TipoPagamento = 1,
                Entrada = DateTime.Now.ToString(),
                Ticket = "123456"
            };

            var movimentacao = Mapper.Map<Movimentacao>(movimentacaoForm);
            movimentacao.Vaga.CategoriaVaga = new CategoriaVaga() { Id = 1 };

            movimentacaoForm = Mapper.Map<MovimentacaoSaidaForm>(movimentacao);

        }
    }
}
