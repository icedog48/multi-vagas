using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Tests.Integration.Selenium.Helpers;
using Xunit;

namespace Tests.Integration.Selenium
{
    public class FuncionarioTest : ScreenTest, IDisposable
    {
        [Fact(DisplayName = "Como membro da equipe multivagas, devo poder cadastar um funcionario administrador para um estacionamento")]
        public void ComoAdministradorDeveCadastrarFuncionarioAdmnistradorDoEstacionamento ()
        {
            FazerLoginComoEquipeMultivagas();

            wait.Until(x => ExpectedConditions.ElementIsVisible(By.Name("btnNovoEstacionamento")));

            ScreenTestHelper.ClickElementByName(driver, "menuSettings");
            ScreenTestHelper.ClickElementByName(driver, "MenuFuncionarios");

            wait.Until(x => ExpectedConditions.ElementIsVisible(By.Name("btnAdicionar")));
            ScreenTestHelper.ClickElementByName(driver, "btnAdicionar");

            ScreenTestHelper.WaitForElement(driver, "Estacionamento");

            var estacionamento = new SelectElement(driver.FindElement(By.Name("Estacionamento")));
                estacionamento.SelectByIndex(1);

                driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(10));

            var funcionario = DateTime.Now.ToString("ddMMyyyymm");

            ScreenTestHelper.FillTextBoxByName(driver, "Nome", "Funcionario " + funcionario);
            ScreenTestHelper.FillTextBoxByName(driver, "CPF", funcionario);
            ScreenTestHelper.FillTextBoxByName(driver, "Telefone", "123456789");            

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
            driver.Quit();
        }
    }
}
