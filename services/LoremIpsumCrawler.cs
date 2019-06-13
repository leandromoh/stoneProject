using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using IServices;

namespace Services
{
    public class LoremIpsumCrawler : ILoremIpsumService
    {
        // https://gist.github.com/andyyou/3052671
        // https://stackoverflow.com/questions/10161413/headless-browser-for-c-sharp-net

        public string GenerateLoremIpsum(bool startWithLoremIpsum, int quantityOfParagraphs, ParagraphSize paragraphSize)
        {
            // https://stackoverflow.com/a/53830629 
            var path = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            
            using (IWebDriver driver = new FirefoxDriver(path))
            {
                driver.Navigate().GoToUrl("http://www.loremipzum.com/pt/gerador-de-texto");

                SetConfigurations(driver, startWithLoremIpsum, quantityOfParagraphs, paragraphSize);

                return GetOutputtedLorem(driver);
            }
        }

        void SetConfigurations(IWebDriver driver, bool startWithLoremIpsum, int quantityOfParagraphs, ParagraphSize paragraphSize)
        {
            SetStartWithLorem(driver, startWithLoremIpsum);
            SetParagraphSize(driver, paragraphSize);
            SetQuantityOfParagraphs(driver, quantityOfParagraphs);
        }

        void SetStartWithLorem(IWebDriver driver, bool startWithLoremIpsum)
        {
            IWebElement chkStartWithLorem = driver.FindElement(By.CssSelector("input[type=checkbox][name=st]"));

            if (chkStartWithLorem.Selected != startWithLoremIpsum)
            {
                chkStartWithLorem.Click();
            }
        }

        void SetParagraphSize(IWebDriver driver, ParagraphSize paragraphSize)
        {
            IEnumerable<IWebElement> rdoSizeParagraphs = driver.FindElements(By.CssSelector("input[name=plenght]"))
                                                               .OrderBy(x => x.GetProperty("value"));

            rdoSizeParagraphs.ElementAt((int)paragraphSize - 1).Click();
        }

        void SetQuantityOfParagraphs(IWebDriver driver, int quantityOfParagraphs)
        {
            IWebElement txtNumberParagraphs = driver.FindElement(By.Id("pnumber"));

            txtNumberParagraphs.Clear();
            txtNumberParagraphs.SendKeys(quantityOfParagraphs.ToString());
        }

        string GetOutputtedLorem(IWebDriver driver)
        {
            IWebElement btnGenerate = driver.FindElement(By.Id("generate2"));
            btnGenerate.Click();

            // confirmar se realmente espera
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(1));

            return driver.FindElement(By.Id("outputtext")).Text;
        }
    }
}
