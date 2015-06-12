using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tests.Integration.Selenium.Helpers;
using Model;
using StructureMap;
using Service.Filters;
using Service.Interfaces;
using System.Diagnostics;

namespace Tests.Integration.Selenium
{
    public struct  Usuario 
    {
        public string Email { get; set; }

        public string Senha { get; set; }
    }

    public abstract class ScreenTest
    {
        protected IWebDriver driver;
        protected WebDriverWait wait;
        protected string urlApp = "http://localhost:57625";

        public Usuario EquipeMultivagas
        {
            get
            {
                return new Usuario() { Email = "multivagas@multivagas.com", Senha = "multivagas" };
            }
        }

        public ScreenTest()
        {
            container = IoCHelper.Initialize();
        }

        /// <summary>
        /// Realiza login com usuario da equipe multivagas
        /// </summary>
        protected virtual void FazerLoginComoEquipeMultivagas()
        {
            FazerLoginComoEquipeMultivagas(EquipeMultivagas.Email, EquipeMultivagas.Senha);
        }

        protected virtual void FazerLoginComoEquipeMultivagas(string usuario, string senha)
        {
            driver = new ChromeDriver();

            driver.Navigate().GoToUrl(urlApp);

            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));
            wait.Until(x => ExpectedConditions.ElementIsVisible(By.Name("btnLogin")));

            ScreenTestHelper.ClickElementByName(driver, "btnLogin");

            wait.Until(x => ExpectedConditions.ElementIsVisible(By.Name("btnLogin")));

            ScreenTestHelper.FillTextBoxByName(driver, "Email", usuario);
            ScreenTestHelper.FillTextBoxByName(driver, "Senha", senha);

            ScreenTestHelper.ClickElementByName(driver, "btnLogin");
        }

        #region Estacionamento Teste

        protected IContainer container;

        protected string RazaoSocialEstacionamentoTeste = "Estacionamento Teste - Cadastro Funcionario";

        protected virtual Estacionamento ObterEstacionamentoTeste()
        {
            var estacionamentoService = container.GetInstance<IEstacionamentoService>();

            var filtro = new EstacionamentoFilter()
            {
                RazaoSocial = RazaoSocialEstacionamentoTeste
            };

            var estacionamentos = estacionamentoService.GetByFilter(filtro);

            if (estacionamentos.Any()) return estacionamentos.First();

            var estacionamentoDeTeste = new Estacionamento()
            {
                Bairro = "Bangu",
                CEP = "21831345",
                Cidade = "Rio de Janeiro",
                CNPJ = "123456789",
                Email = "teste@multivagas.com",
                Logradouro = "Rua dos Zueros",
                RazaoSocial = RazaoSocialEstacionamentoTeste,
                Telefone = "123456789",
                UF = "RJ",
                Usuario = new Model.Usuario() { NomeUsuario = "admin_teste", Email = "admin_teste@multivagas.com", AlterarSenha = false },
            };

            estacionamentoService.Add(estacionamentoDeTeste);

            return estacionamentoDeTeste;
        }

        #endregion Estacionamento Teste

        protected virtual void QuitWebDriver()
        {
            try
            {
                driver.Quit();

                var processList = Process.GetProcessesByName("chromedriver");

                processList.ToList().ForEach(x => x.Kill());
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
