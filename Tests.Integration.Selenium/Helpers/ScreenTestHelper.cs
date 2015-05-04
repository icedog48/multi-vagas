using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
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
            try
            {
                WaitLoop(indice =>
                {
                    var element = driver.FindElement(By.Name(name));

                    element.Click();
                });
            }
            catch (Exception ex)
            {
                var msgErro = string.Format("Erro clicando elemento.: {0}", name);
                throw new InvalidOperationException(msgErro, ex);
            }
        }

        public static void FillTextBoxByName(IWebDriver driver, string name, string value)
        {
            try
            {
                WaitLoop(indice =>
                {
                    var textbox = driver.FindElement(By.Name(name));

                    textbox.SendKeys(value);
                });
            }
            catch (Exception ex)
            {
                var msgErro = string.Format("Erro preenchendo elemento.: {0}, com valor '{1}'", name, value);
                throw new InvalidOperationException(msgErro, ex);
            }
            
        }
        
        public static void WaitForAlert(IWebDriver driver)
        {
            try
            {
                WaitLoop(indice =>
                {
                    var alert = driver.SwitchTo().Alert();
                });
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Erro esperando por alerta.", ex);
            }
        }       

        public static void WaitForElement(IWebDriver driver, string name)
        {
            try
            {
                WaitLoop(indice =>
                {
                    var alert = driver.FindElement(By.Name(name));
                });
            }
            catch (Exception ex)
            {
                var msgErro = string.Format("Erro esperando por elemento.: {0}", name);
                throw new InvalidOperationException(msgErro, ex);
            }
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

            if (!achou) throw exception;
        }

        #endregion 
    
        public static Exception exception { get; set; }

        public static void ChooseElementInList(IWebDriver driver, string name, int elementIndex)
        {
            try
            {
                WaitLoop(indice =>
                {
                    var selectElement = new SelectElement(driver.FindElement(By.Name(name)));
                        selectElement.SelectByIndex(elementIndex);
                });
            }
            catch (Exception ex)
            {
                var msgErro = string.Format("Erro selecionando inddice, {0} no elemento.: {1}", elementIndex, name);
                
                throw new InvalidOperationException(msgErro, ex);
            }
        }
    }
}
