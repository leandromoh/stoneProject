using System;

namespace IServices
{
    public enum ParagraphSize
    {
        Short = 1,
        Medium = 2,
        Long = 3,
    }

    public interface ILoremIpsumService
    {
        string GenerateLoremIpsum(bool startWithLoremIpsum, int quantityOfParagraphs, ParagraphSize paragraphSize);
    }
}
