using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using IServices;

namespace Services
{
    public class LoremIpsumCrawler : ILoremIpsumService
    {
        private IWebDriverFactory _webDriverFactory;

        public LoremIpsumCrawler(IWebDriverFactory WebDriverFactory)
        {
            _webDriverFactory = WebDriverFactory;
        }

        public string GenerateLoremIpsum(bool startWithLoremIpsum, int quantityOfParagraphs, ParagraphSize paragraphSize)
        {
            using (IWebDriver driver = _webDriverFactory.GetDriver())
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
                                                        .OrderBy(x => x.GetAttribute("value"));

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
