using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using System;
using IServices;

namespace Services
{
    public class ByteCounterCrawler : IByteCounterService
    {
        public long CountBytes(string text)
        {
            var path = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);

            using (IWebDriver driver = new FirefoxDriver(path))
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
