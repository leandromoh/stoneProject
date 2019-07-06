using System.Collections.Generic;
using IServices;

namespace Services
{
    public class ByteCounterFallBackManager : BaseFallBackManager<IByteCounterService>, IByteCounterService
    {
        public ByteCounterFallBackManager(params IByteCounterService[] Source)
            : base(new List<IByteCounterService>(Source))
        {
        }

        public int CountBytes(string text)
        {
            return base.TryForAllSources(s => s.CountBytes(text));
        }
    }
}
