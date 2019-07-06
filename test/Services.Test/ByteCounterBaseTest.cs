using System;
using Services;
using IServices;
using NUnit.Framework;
using System.Text;

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

        [TestCase("i ♥ u")]
        [TestCase("ユーザー別サイト")]
        [TestCase("简体中文")]
        [TestCase("크로스 플랫폼으로")]
        [TestCase("מדורים מבוקשים")]
        [TestCase("أفضل البحوث")]
        [TestCase("Σὲ γνωρίζω ἀπὸ")]
        [TestCase("Десятую Международную")]
        [TestCase("แผ่นดินฮั่นเสื่อมโทรมแสนสังเวช")]
        [TestCase("∮ E⋅da = Q, n → ∞, ∑ f(i) = ∏ g(i)")]
        [TestCase("français langue étrangère")]
        [TestCase("mañana olé")]
        public void CountBytes(string text)
        {
            var result = counter.CountBytes(text);
            var expecting = Encoding.UTF8.GetByteCount(text);

            Assert.That(result, Is.EqualTo(expecting));
        }
    }
}
