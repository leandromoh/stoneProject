using System;
using Services;
using IServices;
using NUnit.Framework;

namespace Services.Test
{
    [TestFixture]
    public abstract class ByteCounterBaseTest
    {
        private IByteCounterService counter;

        protected abstract IByteCounterService GetInstance();

        [SetUp]
        public void Init()
        {
            counter = GetInstance();
        }

        [TestCase("i ♥ u", 7)]
        [TestCase("ユーザー別サイト", 24)]
        [TestCase("简体中文", 12)]
        [TestCase("크로스 플랫폼으로", 25)]
        [TestCase("מדורים מבוקשים", 27)]
        [TestCase("أفضل البحوث", 21)]
        [TestCase("Σὲ γνωρίζω ἀπὸ", 29)]
        [TestCase("Десятую Международную", 41)]
        [TestCase("แผ่นดินฮั่นเสื่อมโทรมแสนสังเวช", 90)]
        [TestCase("∮ E⋅da = Q, n → ∞, ∑ f(i) = ∏ g(i)", 46)]
        [TestCase("français langue étrangère", 28)]
        [TestCase("mañana olé", 12)]
        public void CountBytes(string text, int expecting)
        {
            Assert.That(counter.CountBytes(text), Is.EqualTo(expecting));
        }
    }
}
