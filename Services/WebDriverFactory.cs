using OpenQA.Selenium;
using OpenQA.Selenium.PhantomJS;
using System.IO;
using System.Reflection;

namespace Services
{
    public interface IWebDriverFactory
    {
        IWebDriver GetDriver();
    }

    public class PhantomJSFactory : IWebDriverFactory
    {
        public IWebDriver GetDriver()
        {
            return new PhantomJSDriver(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
        }
    }
}
