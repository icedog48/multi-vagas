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
            int i = 0;

            while (i++ < 5)
            {
                try
                {
                    var element = driver.FindElement(By.Name(name));
                    
                    element.Click();

                    break;
                }
                catch (Exception e)
                {
                    Thread.Sleep(1000);
                    continue;
                }
            }
        }

        public static void FillTextBoxByName(IWebDriver driver, string name, string value)
        {
            int i = 0;

            while (i++ < 5)
            {
                try
                {
                    var textbox = driver.FindElement(By.Name(name));
                    
                    textbox.SendKeys(value);

                    break;
                }
                catch (Exception e)
                {
                    Thread.Sleep(1000);
                    continue;
                }
            }
        }

        public static void WaitForAlert(IWebDriver driver)
        {
            int i = 0;

            while (i++ < 5)
            {
                try
                {
                    var alert = driver.SwitchTo().Alert();
                    break;
                }
                catch (NoAlertPresentException e)
                {
                    Thread.Sleep(1000);
                    continue;
                }
            }
        }

        public static void WaitForElement(IWebDriver driver, string name)
        {
            int i = 0;

            while (i++ < 5)
            {
                try
                {
                    var alert = driver.FindElement(By.Name(name));
                    break;
                }
                catch (NoSuchElementException e)
                {
                    Thread.Sleep(1000);
                    continue;
                }
            }
        }
    }
}
