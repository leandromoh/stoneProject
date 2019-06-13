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

            var text = service.GenerateLoremIpsum(true, 3, ParagraphSize.Short);

            Console.WriteLine(text);
        }
    }
}
