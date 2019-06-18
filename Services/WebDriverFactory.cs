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
            var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var service = PhantomJSDriverService.CreateDefaultService(path);
            
            service.AddArgument("--webdriver-loglevel=NONE");
            service.AddArgument("--web-security=false");
            service.AddArgument("--ssl-protocol=any");
            service.AddArgument("--ignore-ssl-errors=true");
            service.AddArgument("--load-images=false");

            return new PhantomJSDriver(service);
        }
    }
}
