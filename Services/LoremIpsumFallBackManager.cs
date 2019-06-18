using System.Collections.Generic;
using IServices;

namespace Services
{
    public class LoremIpsumFallBackManager : BaseFallBackManager<ILoremIpsumService>, ILoremIpsumService
    {
        public LoremIpsumFallBackManager(params ILoremIpsumService[] Source)
            : base(new List<ILoremIpsumService>(Source))
        {
        }

        public string GenerateLoremIpsum(bool startWithLoremIpsum, int quantityOfParagraphs, ParagraphSize paragraphSize)
        {
            return base.TryForAllSources(s => s.GenerateLoremIpsum(startWithLoremIpsum, quantityOfParagraphs, paragraphSize));
        }
    }
}
