using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Remote;
using System.Diagnostics;
using UIT.iDeal.Common.Helpers;
using UIT.iDeal.TestLibrary.ProcessLifecycleManagement;

namespace UIT.iDeal.TestLibrary.Browsers
{
    public class BrowserDriver
    {
        readonly Func<IWebDriver> _browserFactory;

        public static readonly BrowserDriver InternetExplorer;
        public static readonly BrowserDriver Firefox;

        public static readonly BrowserDriver Chrome;

        static Process _browserDriverProcess;

        public static void KillChromeDriverProcess()
        {
            if (_browserDriverProcess != null)
            {
                _browserDriverProcess.Kill();
            }
        }

        static BrowserDriver()
        {
            InternetExplorer = new BrowserDriver(() => BrowserFactory(@"InternetExplorerDriver\IEDriverServer.exe", DesiredCapabilities.InternetExplorer()));

            Firefox = new BrowserDriver(() =>
            {
                var profile = new FirefoxProfile();

                return new FirefoxDriver(profile);
            });

            Chrome = new BrowserDriver(() => BrowserFactory());
        }

        BrowserDriver(Func<IWebDriver> browserFactory)
        {
            _browserFactory = browserFactory;
        }

        public IWebDriver CreateDriver()
        {
            return _browserFactory();
        }

        private static IWebDriver BrowserFactory(string driverFileName = @"Chromedriver\ChromeDriver.exe", DesiredCapabilities browserCapabilities = null)
        {

            var startInfo = new ProcessStartInfo
            {
                WindowStyle = ProcessWindowStyle.Normal,
                ErrorDialog = false,
                CreateNoWindow = false,
                UseShellExecute = false,
                FileName = string.Format("{0}\\{1}", FileHelpers.GetProjectFolderPath("thirdparty"),driverFileName)
            };

            _browserDriverProcess = new Process { StartInfo = startInfo };
            _browserDriverProcess.Start();
            MortalChildProcessList.AddProcess(_browserDriverProcess.Id);

            var capabilities = browserCapabilities ?? DesiredCapabilities.Chrome();

            var driver = new RemoteWebDriver(new Uri("http://localhost:9515"), capabilities);

            return driver;
        }
    }
}