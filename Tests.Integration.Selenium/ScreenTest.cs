using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tests.Integration.Selenium.Helpers;

namespace Tests.Integration.Selenium
{
    public abstract class ScreenTest
    {
        protected IWebDriver driver;
        protected WebDriverWait wait;
        protected string urlApp = "http://localhost:57625/";

        protected virtual void FazerLoginComoEquipeMultivagas()
        {
            driver = new ChromeDriver();

            driver.Navigate().GoToUrl(urlApp);

            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));
            wait.Until(x => ExpectedConditions.ElementIsVisible(By.Name("btnLogin")));

            ScreenTestHelper.FillTextBoxByName(driver, "Login", "admin");
            ScreenTestHelper.FillTextBoxByName(driver, "Senha", "multivagas");

            ScreenTestHelper.ClickElementByName(driver, "btnLogin");
        }
    }
}
