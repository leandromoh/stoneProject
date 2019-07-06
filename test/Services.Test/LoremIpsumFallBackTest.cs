using System;
using Services;
using IServices;
using NUnit.Framework;

namespace Services.Test
{
    [TestFixture]
    public class LoremIpsumFallBackTest : LoremIpsumBaseTest
    {
        protected override ILoremIpsumService GetInstance()
        {
            return new LoremIpsumFallback();
        }
    }
}
