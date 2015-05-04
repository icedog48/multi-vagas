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
            this.container = IoCHelper.Initialize();
        }

        [Fact(DisplayName="Movimentação - Automapper")]
        public void DeveMapearEntidadeMovimentacao() 
        {
            Automapper.Setup();

            var movimentacaoForm = new MovimentacaoForm()
            {
                Cliente = null,
                CategoriaVaga = 1,
                Vaga = 1,
                Placa = "1234"
            };

            var movimentacao = Mapper.Map<Movimentacao>(movimentacaoForm);
                movimentacao.Vaga.CategoriaVaga = new CategoriaVaga() { Id = 1 };

            movimentacaoForm = Mapper.Map<MovimentacaoForm>(movimentacao);

        }
    }
}
