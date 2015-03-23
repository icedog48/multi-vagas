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
    public class EstacionamentoTest
    {
        [Fact(DisplayName="Como membro da equipe multivagas, devo poder cadastar um estacionamento")]
        public void ComoAdministradorDeveCadastrarEstacionamento ()
        {
            var driver = new ChromeDriver();

            driver.Navigate().GoToUrl("http://localhost:57625/");

            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));
            wait.Until(x => ExpectedConditions.ElementIsVisible(By.Name("btnLogin")));

            //Perfil Equipe Multivagas
            ScreenTestHelper.FillTextBoxByName(driver, "Login", "admin");
            ScreenTestHelper.FillTextBoxByName(driver, "Senha", "multivagas");

            ScreenTestHelper.ClickElementByName(driver, "btnLogin");

            wait.Until(x => ExpectedConditions.ElementIsVisible(By.Name("btnNovoEstacionamento")));

            ScreenTestHelper.ClickElementByName(driver, "btnNovoEstacionamento");

            wait.Until(x => ExpectedConditions.ElementIsVisible(By.Name("btnSalvar")));

            ScreenTestHelper.FillTextBoxByName(driver, "CNPJ", DateTime.Now.ToString("ddMMyyyymm"));
            ScreenTestHelper.FillTextBoxByName(driver, "RazaoSocial", "Teste " + DateTime.Now.ToString("ddMMyyyymmss"));
            ScreenTestHelper.FillTextBoxByName(driver, "Telefone", "123456789");
            ScreenTestHelper.FillTextBoxByName(driver, "Email", "email@email.com.br");

            ScreenTestHelper.FillTextBoxByName(driver, "CEP", "123456");
            ScreenTestHelper.FillTextBoxByName(driver, "Logradouro", "Rua Ulpiano dos Santos, 275");
            ScreenTestHelper.FillTextBoxByName(driver, "Bairro", "Bangu");
            ScreenTestHelper.FillTextBoxByName(driver, "Cidade", "Rio de Janeiro");
            ScreenTestHelper.FillTextBoxByName(driver, "UF", "RJ");

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

            driver.Quit();

            Assert.Contains("Operação realizada com sucesso", resultado);
            
        }

    }
}
