using System;
using Services;
using IServices;
using NUnit.Framework;

namespace Services.Test
{
    [TestFixture]
    public class LoremIpsumCrawlerTest : LoremIpsumBaseTest
    {
        protected override ILoremIpsumService GetInstance()
        {
            IWebDriverFactory factory = new PhantomJSFactory();
            ILoremIpsumService loremCrawler = new LoremIpsumCrawler(factory);
            return loremCrawler;
        }
    }
}
