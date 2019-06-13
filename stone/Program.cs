using System;
using Services;
using IServices;

namespace stone
{
    class Program
    {
        static void Main(string[] args)
        {
            ILoremIpsumService service = new LoremIpsumFallback();

            // var text = service.GenerateLoremIpsum(true, 3, ParagraphSize.Short);

            Byte[] encodedBytes = new System.Text.UTF8Encoding().GetBytes("日本語ée");

            IByteCounterService counter = new ByteCounterCrawler();
            long bytes = counter.CountBytes("i ♥ u");

            Console.WriteLine(bytes);
        }
    }
}
