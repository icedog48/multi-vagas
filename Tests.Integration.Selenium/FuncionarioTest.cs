using Model;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using Service.Filters;
using Service.Interfaces;
using StructureMap;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    public class FuncionarioTest : ScreenTest, IDisposable
    {
        public FuncionarioTest()
        {
            
        }

        [Fact(DisplayName = "Cadastrar funcionario - Como membro da equipe multivagas, devo poder cadastar um funcionario administrador para um estacionamento")]
        public void ComoAdministradorDeveCadastrarFuncionarioAdmnistradorDoEstacionamento ()
        {
            var estacionamentoTeste = ObterEstacionamentoTeste();

            FazerLoginComoEquipeMultivagas(estacionamentoTeste.Usuario.NomeUsuario, "multivagas");

            wait.Until(x => ExpectedConditions.ElementIsVisible(By.Name("btnNovoEstacionamento")));

            ScreenTestHelper.ClickElementByName(driver, "menuSettings");
            ScreenTestHelper.ClickElementByName(driver, "MenuFuncionarios");

            wait.Until(x => ExpectedConditions.ElementIsVisible(By.Name("btnAdicionar")));
            ScreenTestHelper.ClickElementByName(driver, "btnAdicionar");

            ScreenTestHelper.WaitForElement(driver, "Estacionamento");

            ScreenTestHelper.ChooseElementInList(driver, "Estacionamento", 1);

            var funcionario = DateTime.Now.ToString("ddMMyyyymm");

            ScreenTestHelper.FillTextBoxByName(driver, "Nome", "Funcionario " + funcionario);
            ScreenTestHelper.FillTextBoxByName(driver, "CPF", funcionario);
            ScreenTestHelper.FillTextBoxByName(driver, "Telefone", "123456789");
            ScreenTestHelper.FillTextBoxByName(driver, "Email", funcionario + "@multivagas.com.br");

            ScreenTestHelper.FillTextBoxByName(driver, "CEP", "123456");
            ScreenTestHelper.FillTextBoxByName(driver, "Logradouro", "Rua Ulpiano dos Santos, 275");
            ScreenTestHelper.FillTextBoxByName(driver, "Bairro", "Bangu");
            ScreenTestHelper.FillTextBoxByName(driver, "Cidade", "Rio de Janeiro");
            ScreenTestHelper.FillTextBoxByName(driver, "UF", "RJ");                             

            ScreenTestHelper.FillTextBoxByName(driver, "Matricula", funcionario);
            ScreenTestHelper.FillTextBoxByName(driver, "HoraInicio", "08:00");
            ScreenTestHelper.FillTextBoxByName(driver, "HoraSaida", "18:00");
            ScreenTestHelper.FillTextBoxByName(driver, "CargaHoraria", "08:00");
            ScreenTestHelper.FillTextBoxByName(driver, "DataAdmissao", DateTime.Now.ToString("dd/MM/yyyy"));
            ScreenTestHelper.FillTextBoxByName(driver, "Salario", "500,00");            

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
