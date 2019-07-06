using System;
using System.IO;
using System.Text;
using Services;
using IServices;
using Microsoft.Extensions.Configuration;

namespace stone
{
    public class FileWriterManager
    {
        private readonly ILoremIpsumService _loremIpsumService;
        private readonly IByteCounterService _byteCounterService;
        private readonly int _bufferSizeInBytes;
        private readonly int _maxFileSizeInBytes;
        private readonly string _filePath;

        public FileWriterManager(
            ILoremIpsumService LoremIpsumService,
            IByteCounterService ByteCounterService,
            string FilePath,
            int bufferSizeInMB = 1,
            int maxFileSizeInMB = 10)
        {
            _loremIpsumService = LoremIpsumService;
            _byteCounterService = ByteCounterService;
            _filePath = FilePath;
            _bufferSizeInBytes = bufferSizeInMB * 1024 * 1024;
            _maxFileSizeInBytes = maxFileSizeInMB * 1024 * 1024;
        }

        public int WriteLoremFile(bool startWithLoremIpsum, int quantityOfParagraphs, ParagraphSize paragraphSize)
        {
            var iterations = 0;

            using (var fileStream = new StreamWriter(_filePath, true, Encoding.UTF8))
            {
                var totalBytesWritten = 0L;
                var bytesInBuffer = 0L;

                while (true)
                {
                    string lorem = _loremIpsumService.GenerateLoremIpsum(startWithLoremIpsum, quantityOfParagraphs, paragraphSize);
                    long newBytes = _byteCounterService.CountBytes(lorem);

                    if (totalBytesWritten + newBytes > _maxFileSizeInBytes)
                    {
                        fileStream.Flush();
                        break;
                    }

                    if (bytesInBuffer + newBytes > _bufferSizeInBytes)
                    {
                        fileStream.Flush();
                        bytesInBuffer = 0;
                    }

                    totalBytesWritten += newBytes;
                    bytesInBuffer += newBytes;

                    fileStream.Write(lorem);
                    iterations++;
                }
            }

            return iterations;
        }
    }
}
