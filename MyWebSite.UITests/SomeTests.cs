using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using System;
using System.IO;
using System.Reflection;
using Xunit;

namespace MyWebSite.UITests
{
    public class SomeTests : IDisposable
    {
        private IWebDriver _ieDriver;
        private IWebDriver _chromeDriver;
        private IWebDriver _firefoxDriver;

        private readonly string _appUrl;

        public SomeTests()
        {
            _appUrl = "http://localhost:60459/";
        }

        [Theory]
        //[InlineData("IE")]
        [InlineData("Chrome")]
        //[InlineData("Firefox")]
        public void MultiBrowserTest(string browser)
        {
            var driver = GetDriver(browser);

            driver.Navigate().GoToUrl(_appUrl);

            driver.FindElement(By.LinkText("Home")).Click();
            driver.FindElement(By.LinkText("Privacy")).Click();
            driver.FindElement(By.LinkText("Home")).Click();
            //Thread.Sleep(3000);
            driver.FindElement(By.LinkText("building Web apps with ASP.NET Core")).Click();

            if (browser != "IE")
                driver.Navigate().Back();

            driver.Quit();
        }

        [Fact]
        public void ClickAcceptLink()
        {
            GetDriver().Navigate().GoToUrl(_appUrl);

            GetDriver().FindElement(By.XPath("(.//*[normalize-space(text()) and normalize-space(.)='Learn More'])[1]/following::span[1]")).Click();
        }

        public void Dispose()
        {
            if (_chromeDriver != null)
                _chromeDriver.Quit();

            if (_firefoxDriver != null)
                _firefoxDriver.Quit();

            if (_ieDriver != null)
                _ieDriver.Quit();
        }

        private IWebDriver GetDriver(string browser = "Chrome")
        {
            string binDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            switch (browser)
            {
                case "Firefox":

                    if (_firefoxDriver == null)
                        _firefoxDriver = new FirefoxDriver(binDir);

                    return _firefoxDriver;

                case "IE":

                    if (_ieDriver == null)
                        _ieDriver = new InternetExplorerDriver(binDir);

                    return _ieDriver;

                default:

                    if (_chromeDriver == null)
                        _chromeDriver = new ChromeDriver(binDir);

                    return _chromeDriver;
            }
        }
    }
}