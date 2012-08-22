using System.Threading;
using System;
using OpenQA.Selenium;

namespace UIT.iDeal.TestLibrary.Browsers.Helpers
{
    public class WaitFor
    {
        public static void Seconds(int seconds)
        {
            Thread.Sleep(1000 * seconds);
        }

        public static void AjaxCallsToComplete(IWebDriver driver, int timeOut = 60)
        {
            var end = DateTime.Now + TimeSpan.FromSeconds(timeOut);
            var complete = false;
            
            while (DateTime.Now < end)
            {
                var ajaxIsComplete = (bool)(driver as IJavaScriptExecutor).ExecuteScript("return jQuery.active == 0");
                if (ajaxIsComplete)
                {
                    complete = true;
                    break;
                }
                Thread.Sleep(100);
            }

            if (!complete)
            {
                throw new TimeoutException("Ajax call didn't complete within the Time Out of " + timeOut + " seconds.");
            }
        }
    }
}

