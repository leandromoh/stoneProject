using System;
using Services;
using IServices;
using NUnit.Framework;

namespace Services.Test
{
    [TestFixture]
    public class ByteCounterCrawlerTest : ByteCounterBaseTest
    {
        protected override IByteCounterService GetInstance()
        {
            IWebDriverFactory factory = new PhantomJSFactory();
            IByteCounterService counterCrawler = new ByteCounterCrawler(factory);
            return counterCrawler;
        }
    }
}
