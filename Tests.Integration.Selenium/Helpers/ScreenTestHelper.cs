using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Tests.Integration.Selenium.Helpers
{
    public static class ScreenTestHelper
    {
        public static void ClickElementByName(IWebDriver driver, string name)
        {
            WaitLoop(indice =>
            {
                var element = driver.FindElement(By.Name(name));

                element.Click();
            });
        }

        public static void FillTextBoxByName(IWebDriver driver, string name, string value)
        {
            WaitLoop(indice =>
            {
                var textbox = driver.FindElement(By.Name(name));

                textbox.SendKeys(value);
            });
        }
        
        public static void WaitForAlert(IWebDriver driver)
        {
            WaitLoop(indice =>
            {
                var alert = driver.SwitchTo().Alert();
            });
        }       

        public static void WaitForElement(IWebDriver driver, string name)
        {
            WaitLoop(indice => 
            {
                var alert = driver.FindElement(By.Name(name));
            });
        }

        #region WaitLoops

        private static void WaitLoop(Action<int> code)
        {
            var i = 0;

            var achou = false;

            Exception exception = null;

            while (i++ < 5 && !achou)
            {
                try
                {
                    code(i);

                    achou = true;
                }
                catch (Exception e)
                {
                    exception = e;
                    Thread.Sleep(1000);
                    continue;
                }
            }

            if (!achou) throw new InvalidOperationException("Erro executando operação: " + code.Method.Name, exception);
        }

        #endregion 
    
        public static Exception exception { get; set; }
    }
}
