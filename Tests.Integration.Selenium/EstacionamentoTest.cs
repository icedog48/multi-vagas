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
    public class EstacionamentoTest : ScreenTest, IDisposable
    {
        [Fact(DisplayName="Cadastrar Estacionamento - Como membro da equipe multivagas, devo poder cadastar um estacionamento")]
        public void ComoAdministradorDeveCadastrarEstacionamento ()
        {
            FazerLoginComoEquipeMultivagas();

            wait.Until(x => ExpectedConditions.ElementIsVisible(By.Name("btnNovoEstacionamento")));

            ScreenTestHelper.ClickElementByName(driver, "btnNovoEstacionamento");

            wait.Until(x => ExpectedConditions.ElementIsVisible(By.Name("btnSalvar")));

            var CNPJ = DateTime.Now.ToString("ddMMyyyyHHmmss");

            //Dados Empresariais
            ScreenTestHelper.FillTextBoxByName(driver, "CNPJ", CNPJ);
            ScreenTestHelper.FillTextBoxByName(driver, "RazaoSocial", "Estacionamento " + CNPJ);
            ScreenTestHelper.FillTextBoxByName(driver, "Telefone", "912345678");
            ScreenTestHelper.FillTextBoxByName(driver, "Email", CNPJ + "@estcionamento.com.br");

            //Endereço
            ScreenTestHelper.FillTextBoxByName(driver, "CEP", "21830205");
            ScreenTestHelper.FillTextBoxByName(driver, "Logradouro", "Rua Ulpiano dos Santos, 275");
            ScreenTestHelper.FillTextBoxByName(driver, "Bairro", "Bangu");
            ScreenTestHelper.FillTextBoxByName(driver, "Cidade", "Rio de Janeiro");
            ScreenTestHelper.FillTextBoxByName(driver, "UF", "RJ");

            //Administrador
            ScreenTestHelper.FillTextBoxByName(driver, "Login", CNPJ);
            ScreenTestHelper.FillTextBoxByName(driver, "EmailUsuario", CNPJ + "@multivagas.com.br");

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

            Assert.Contains("Operação realizada com sucesso", resultado);
        }

        public void Dispose()
        {
            QuitWebDriver();
        }

    }
}
