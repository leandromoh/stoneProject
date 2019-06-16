using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using IServices;
using System.Text.RegularExpressions;

namespace Services
{
    public class ByteCounterCrawler : IByteCounterService
    {
        private IWebDriverFactory _webDriverFactory;
        private Regex _nonDigits;

        public ByteCounterCrawler(IWebDriverFactory WebDriverFactory)
        {
            _webDriverFactory = WebDriverFactory;
            _nonDigits = new Regex(@"\D", RegexOptions.Compiled);
        }

        public long CountBytes(string text)
        {
            using (IWebDriver driver = _webDriverFactory.GetDriver())
            {
                driver.Navigate().GoToUrl("https://mothereff.in/byte-counter#");

                SetConfigurations(driver, text);

                return GetOutputtedCount(driver);
            }
        }

        void SetConfigurations(IWebDriver driver, string text)
        {
            IWebElement txt = driver.FindElement(By.CssSelector("textarea"));
            txt.Clear();
            txt.SendKeys(text);
        }

        long GetOutputtedCount(IWebDriver driver)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(1));

            string count = driver.FindElement(By.Id("bytes")).Text;

            return long.Parse(_nonDigits.Replace(count, string.Empty));
        }
    }
}
