using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using System;
using IServices;
using System.Text;

namespace Services
{
    public class ByteCounterFallback : IByteCounterService
    {
        public long CountBytes(string text)
        {
            return Encoding.UTF8.GetBytes(text).LongLength;
        }
    }
}
