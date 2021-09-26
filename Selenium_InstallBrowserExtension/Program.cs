using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Threading;

namespace Selenium_InstallBrowserExtension
{
    class Program
    {
        public static void OpenChromeDriverWithExtension(
            string driverDirectory,
            string extensionPageUrl,
            string installButtonCssSelector
            ) 
        {
            ChromeOptions options = new ChromeOptions();
            options.AddArgument("--start-maximized");

            IWebDriver driver = new ChromeDriver(driverDirectory);
            
            driver.Url = extensionPageUrl;
            Thread.Sleep(2000);

            driver
                .FindElement(By.CssSelector(installButtonCssSelector))
                .Click();
            Thread.Sleep(5000);

            AutoIt.AutoItX.Send("{LEFT}");
            Thread.Sleep(1000);

            AutoIt.AutoItX.Send("{ENTER}");
        }

        static void Main()
        {
            OpenChromeDriverWithExtension(
                @"C:\Program Files\Google\Chrome\Application",
                "https://chrome.google.com/webstore/detail/touch-vpn-secure-and-unli/bihmplhobchoageeokmgbdihknkjbknd?hl=ru",
                "div[role='button'][aria-label='Установить']");

            Console.ReadKey();
        }
    }
}
