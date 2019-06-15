using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using IServices;

namespace Services
{
    public class ByteCounterCrawler : IByteCounterService
    {
        private IWebDriverFactory _webDriverFactory;

        public ByteCounterCrawler(IWebDriverFactory WebDriverFactory)
        {
            _webDriverFactory = WebDriverFactory;
        }

        public long CountBytes(string text)
        {
            using (IWebDriver driver = _webDriverFactory.GetDriver())
            {
                driver.Navigate().GoToUrl("https://mothereff.in/byte-counter#");

                IWebElement txt = driver.FindElement(By.CssSelector("textarea"));
                txt.Clear();
                txt.SendKeys(text);

            // confirmar se realmente espera
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(1));
            
                var result = driver.FindElement(By.Id("bytes")).Text;
                
                return long.Parse(result.Split(' ')[0]);
            }
        }
    }
}
