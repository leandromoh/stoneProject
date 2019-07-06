using System;
using System.IO;
using System.Text;
using Services;
using IServices;
using Microsoft.Extensions.Configuration;
using System.Diagnostics;

namespace stone
{
    class Program
    {
        static void Main(string[] args)
        {
            IConfiguration config = new ConfigurationBuilder()
                                        .SetBasePath(Directory.GetCurrentDirectory())
                                        .AddJsonFile("appsettings.json")
                                        .Build();

            IWebDriverFactory factory = new PhantomJSFactory();
            ILoremIpsumService loremCrawler = new LoremIpsumCrawler(factory, config["loremIpsumSite"]);
            IByteCounterService counterCrawler = new ByteCounterCrawler(factory, config["byteCounterSite"]);

            ILoremIpsumService loremFallback = new LoremIpsumFallback();
            IByteCounterService counterFallback = new ByteCounterFallback();

            ILoremIpsumService loremFallbackManager = new LoremIpsumFallBackManager(loremCrawler, loremFallback);
            IByteCounterService counterFallbackManager = new ByteCounterFallBackManager(counterCrawler, counterFallback);

            var path = @"C:\Users\Leandro\Desktop\lorem.txt";

            if (!int.TryParse(config["bufferSizeInMB"], out var bufferSizeInMB))
            {
                bufferSizeInMB = 1;
            }

            if (!int.TryParse(config["maxFileSizeInMB"], out var maxFileSizeInMB))
            {
                maxFileSizeInMB = 100;
            }

            int iterations = 0;

            TimeSpan elapsedTime = GetExecutionTime(() =>
            {
                var writter = new FileWriterManager(loremFallbackManager, counterFallbackManager, path, bufferSizeInMB, maxFileSizeInMB);

                iterations = writter.WriteLoremFile(true, 8, ParagraphSize.Long);
            });

            Console.Clear();
            Console.WriteLine($"numero de iterações realizadas: {iterations}");
            Console.WriteLine($"tempo total, em segundos, de geração do arquivo: {elapsedTime.TotalSeconds}");
            Console.WriteLine($"tempo médio de escrita: {elapsedTime.TotalSeconds / iterations}");
            Console.WriteLine($"nome arquivo: {Path.GetFileName(path)}");
            Console.WriteLine($"tamanho, em bytes, do arquivo: {new FileInfo(path).Length}");
            Console.WriteLine($"caminho arquivo: {Path.GetDirectoryName(path)}");
            Console.Read();
        }

        public static TimeSpan GetExecutionTime(Action action)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            action();
            stopwatch.Stop();
            return stopwatch.Elapsed;
        }
    }
}
