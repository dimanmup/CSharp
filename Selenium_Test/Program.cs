using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Threading;

namespace Selenium_Test
{
    class Program
    {
        static void Main()
        {
            IWebDriver chrome = new ChromeDriver(@"C:\Program Files\Google\Chrome\Application");
            chrome.Url = "https://neomaster.net";
            var navigation = chrome.Navigate();
            Thread.Sleep(2000);
            navigation.GoToUrl("https://neomaster.net/Audit");
            Thread.Sleep(2000);
        }
    }
}
