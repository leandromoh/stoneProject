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
        private readonly UTF8Encoding encoding = new UTF8Encoding(); 

        public long CountBytes(string text)
        {
            return encoding.GetBytes(text).LongLength;
        }
    }
}
