using System;
using System.Drawing.Imaging;
using System.IO;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Web.Mvc;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using UIT.iDeal.Common.Configuration;
using UIT.iDeal.Common.Extensions;
using UIT.iDeal.Common.Helpers;
using UIT.iDeal.TestLibrary.Browsers.Actions;
using UIT.iDeal.TestLibrary.Browsers.Helpers.Extensions;
using UIT.iDeal.TestLibrary.Configuration.Ioc;

namespace UIT.iDeal.TestLibrary.Browsers.PageObjects.Framework
{
    

    public class UiComponent
    {
        protected internal IWebDriver Browser;

        protected TPage NavigateTo<TPage>(Locators.By clickDestination)
            where TPage : UiComponent, new()
        {
            Navigate(clickDestination);

            return new TPage { Browser = Browser };
        }

        protected TPage NavigateTo<TPage>(By clickDestination)
            where TPage : UiComponent, new()
        {
            Navigate(clickDestination);

            return new TPage { Browser = Browser };
        }

        protected TDestinationPage NavigateTo<TDestinationPage>(string relativeUrl)
            where TDestinationPage : UiComponent, new()
        {
            Browser.Navigate().GoToUrl(relativeUrl);
            return new TDestinationPage { Browser = Browser };
        }
        
        public TComponent GetComponent<TComponent>()
            where TComponent : UiComponent, new()
        {
            return new TComponent() { Browser = Browser };
        }

      

        protected TPage NavigateTo<TPage>(Action actionCausingToNavigate)
            where TPage : Page, new()
        {
            actionCausingToNavigate();
            return new TPage { Browser = Browser };
        }


        protected void Navigate(Locators.By clickDestination)
        {
            Execute(clickDestination, e => e.Click());
        }

        protected void Navigate(By clickDestination)
        {
            Execute(clickDestination, e => e.Click());
        }

        public TReturn ExecuteScriptAndReturn<TReturn>(string javascriptToBeExecuted)
        {
            var javascriptExecutor = ((IJavaScriptExecutor) Browser);
            return (TReturn) javascriptExecutor.ExecuteScript("return " + javascriptToBeExecuted);
        }

        public IWebElement Execute(By findElement, Action<IWebElement> action)
        {
            try
            {
                var element = Browser.FindElement(findElement);
                action(element);
                return element;
            }
            catch (Exception)
            {
                TakeScreenshot();
                throw;
            }
        }

        public TResult Execute<TResult>(By findElement, Func<IWebElement, TResult> func)
        {
            try
            {
                var element = Browser.FindElement(findElement);
                return func(element);
            }
            catch (Exception)
            {
                TakeScreenshot();
                throw;
            }
        }

        protected IWebElement WaitForElementAndExecute(By findElement, Action<IWebElement> action, int waitInSeconds = 20)
        {
            try
            {
                var element = WaitForElement(findElement, waitInSeconds);
                action(element);
                return element;
            }
            catch (Exception)
            {
                TakeScreenshot();
                throw;
            }
        }

        IWebElement WaitForElement(By findElement, int waitInSeconds = 20)
        {
            var wait = new WebDriverWait(Browser, TimeSpan.FromSeconds(waitInSeconds));
            return wait.Until(d => d.FindElement(findElement));
        }

        public void WaitForAjaxCallsToFinish(int timeOutInSecond = 10)
        {
            var end = DateTime.Now + TimeSpan.FromSeconds(timeOutInSecond);
            var complete = false;

            while (DateTime.Now < end)
            {
                var ajaxIsComplete = 
                    (bool)((IJavaScriptExecutor)Browser).ExecuteScript("return jQuery.active == 0");

                if (ajaxIsComplete)
                {
                    complete = true;
                    break;
                }
                Thread.Sleep(100);
            }

            if (!complete)
            {
                throw new TimeoutException("Ajax call didn't complete within the Time Out of " + timeOutInSecond 
                                        + " seconds.");
            }
        }

        public void TakeScreenshot(string fileName = null)
        {
            var pathFromConfig = ConfigSettings.FunctionalScreenShotFolderName.FullTestProjectFolderPath();
            var camera = (ITakesScreenshot)Browser;
            var screenshot = camera.GetScreenshot();

            if (!Directory.Exists(pathFromConfig))
                Directory.CreateDirectory(pathFromConfig);

            var windowTitle = Browser.Title;
            fileName = fileName ?? string.Format("{0}{1}.png", windowTitle, DateTime.Now.ToFileTime()).Replace(':', '.');
            var outputPath = Path.Combine(pathFromConfig, fileName);

            var pathChars = Path.GetInvalidPathChars();

            var stringBuilder = new StringBuilder(outputPath);

            foreach (var item in pathChars)
                stringBuilder.Replace(item, '.');

            var screenShotPath = stringBuilder.ToString();
            screenshot.SaveAsFile(screenShotPath, ImageFormat.Png);
        }

        public void MoveMouseOverElement(string jquerySelector)
        {
            ((RemoteWebDriver)Browser).ExecuteScript(String.Format("$(\"{0}\").trigger('mouseenter')", jquerySelector));
        }


        public void SendShortCutKeys(string commandKey, char key)
        {
            Browser.SendKeyBoardShortCut(commandKey, key);
        }

        public void SendShortCutKeys(string[] commandKeys, char key)
        {
            Browser.SendKeyBoardShortCut(commandKeys, key);
        }

        protected Boolean IsFocused(string elementId)
        {
            var selector = string.Format("#{0}:focus", elementId);

            return Browser.HasElement(Locators.By.jQuery(selector));
        }

        protected static bool CanWriteProperty(String text, PropertyInfo property)
        {
            return !String.IsNullOrEmpty(text) && property != null && property.CanWrite;
        } 

    }

    
}