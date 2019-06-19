using System;
using Services;
using IServices;
using NUnit.Framework;

namespace Services.Test
{
    [TestFixture]
    public class ByteCounterFallBackTest : ByteCounterBaseTest
    {
        protected override IByteCounterService GetInstance()
        {
            return new ByteCounterFallback();
        }
    }
}
