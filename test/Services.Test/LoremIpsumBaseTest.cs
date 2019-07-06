using System;
using Services;
using IServices;
using NUnit.Framework;
using System.Text;
using System.Collections.Generic;
using System.Linq;

namespace Services.Test
{
    [TestFixture]
    public abstract class LoremIpsumBaseTest
    {
        private ILoremIpsumService lorem;

        protected abstract ILoremIpsumService GetInstance();

        [SetUp]
        public void Init()
        {
            lorem = GetInstance();
        }

        [TestCase(true)]
        [TestCase(false)]
        public void GenerateLoremIpsum_StartWithLorem(bool startWithLoremIpsum)
        {
            var text = lorem.GenerateLoremIpsum(startWithLoremIpsum, 3, ParagraphSize.Short);
            var result = text.StartsWith("Lorem ipsum");

            Assert.That(result, Is.EqualTo(startWithLoremIpsum));
        }

        [TestCaseSource(nameof(QuantityOfParagraphsSource))]
        public void GenerateLoremIpsum_QuantityOfParagraphs(bool startWithLoremIpsum, int quantityOfParagraphs, ParagraphSize size)
        {
            var text = lorem.GenerateLoremIpsum(startWithLoremIpsum, quantityOfParagraphs, size);
            var paragraphs = text.Split("\n", StringSplitOptions.RemoveEmptyEntries);

            Assert.That(paragraphs.Length, Is.EqualTo(quantityOfParagraphs));
        }

        public static IEnumerable<object> QuantityOfParagraphsSource()
        {
            return from quantity in Enumerable.Range(0, 30)
                   from startWithLorem in new[] { true, false }
                   from paragraphSize in new[] { ParagraphSize.Short, ParagraphSize.Medium, ParagraphSize.Long }
                   from tcd in new[]
                   {
                       new TestCaseData(startWithLorem, quantity, paragraphSize),
                   }
                   select tcd;
        }
    }
}
