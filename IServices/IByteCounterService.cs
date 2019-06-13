using System;

namespace IServices
{
    public interface IByteCounterService
    {
        long CountBytes(string text);
    }
}
