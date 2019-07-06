using System;

namespace IServices
{
    public enum ParagraphSize
    {
        Short = 3,
        Medium = 6,
        Long = 9,
    }

    public interface ILoremIpsumService
    {
        string GenerateLoremIpsum(bool startWithLoremIpsum, int quantityOfParagraphs, ParagraphSize paragraphSize);
    }
}
