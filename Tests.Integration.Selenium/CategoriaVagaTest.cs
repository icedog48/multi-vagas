using Model;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using Service.Filters;
using Service.Interfaces;
using StructureMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Tests.Integration.Selenium.Helpers;
using Web.DependencyResolution;
using Web.DependencyResolution.Registries;
using Xunit;

namespace Tests.Integration.Selenium
{
    public class CategoriaVagaTest : ScreenTest, IDisposable
    {
        public CategoriaVagaTest()
        {
            
        }

        [Fact(DisplayName = "Cadastrar categoria de vaga - Como administrador de estacionamento, devo poder cadastar categorias de vaga")]
        public void ComoAdministradorDeveCadastrarFuncionarioAdmnistradorDoEstacionamento ()
        {
            var estacionamentoTeste = ObterEstacionamentoTeste();

            FazerLoginComoEquipeMultivagas(estacionamentoTeste.Usuario.Email, "multivagas");

            wait.Until(x => ExpectedConditions.ElementIsVisible(By.Name("btnNovoEstacionamento")));

            ScreenTestHelper.ClickElementByName(driver, "menuCadastros");
            ScreenTestHelper.ClickElementByName(driver, "MenuVagas");

            wait.Until(x => ExpectedConditions.ElementIsVisible(By.Name("btnAdicionar")));
            
            ScreenTestHelper.ClickElementByName(driver, "btnAdicionar");

            ScreenTestHelper.WaitForElement(driver, "Estacionamento");

            ScreenTestHelper.ChooseElementInList(driver, "Estacionamento", 1);

            var categoria = DateTime.Now.ToString("ddMMyyyymm");
            var sigla = DateTime.Now.ToString("mmss");

            ScreenTestHelper.FillTextBoxByName(driver, "Descricao", "Categoria " + categoria);
            ScreenTestHelper.FillTextBoxByName(driver, "Sigla", "SGL");
            ScreenTestHelper.FillTextBoxByName(driver, "Quantidade", "10");
            ScreenTestHelper.FillTextBoxByName(driver, "ValorHora", "5,00");

            var resultado = string.Empty;

            try
            {
                ScreenTestHelper.ClickElementByName(driver, "btnSalvar");

                ScreenTestHelper.WaitForAlert(driver);

                driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(10));

                var alert = driver.SwitchTo().Alert();

                resultado = alert.Text;

                alert.Accept();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            Thread.Sleep(5000);            

            Assert.Contains("Operação realizada com sucesso", resultado);            
        }

        public void Dispose()
        {
            QuitWebDriver();
        }
    }
}
