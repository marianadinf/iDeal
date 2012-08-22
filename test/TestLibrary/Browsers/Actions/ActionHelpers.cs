using System;
using System.Text;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using UIT.iDeal.TestLibrary.Browsers.Locators;

namespace UIT.iDeal.TestLibrary.Browsers.Actions
{
    public static class ActionHelpers 
    {
       
        
        public static void SendKeyBoardShortCut(this IWebDriver browser, string commandKey, char key )
        {
            browser.SendKeyBoardShortCut(new[]{commandKey}, key);
        }

        public static TReturn ExecuteJavaScriptAndReturn<TReturn>(this IWebDriver browser, string javascriptToBeExecuted)
        {
            var remoteDriver = (RemoteWebDriver)browser;

            var x = remoteDriver.ExecuteScript("return " + javascriptToBeExecuted);

            return (TReturn) x;
        }

        public static void SendKeyBoardShortCut(this IWebDriver browser, string[] commandKeys, char key)
        {

            var remoteDriver = (RemoteWebDriver)browser;

            var javaScriptBuilder = new StringBuilder("var keypress = $.Event(\"keydown\");");

            foreach (var commandKey in commandKeys)
            {
                var keyCombination = String.Empty;
                
                if (commandKey == Keys.Control || commandKey == Keys.LeftControl)
                {
                    keyCombination = "ctrlKey";
                }

                if (commandKey == Keys.Alt || commandKey == Keys.LeftAlt)
                {
                    keyCombination = "altKey";
                }

                if (commandKey == Keys.Shift || commandKey == Keys.LeftShift)
                {
                    keyCombination = "shiftKey";
                }


                if (!String.IsNullOrEmpty(keyCombination))
                {
                    javaScriptBuilder.AppendFormat("keypress.{0} = true;", keyCombination);
                }
            }
            
            
            javaScriptBuilder.AppendFormat("keypress.which = {0};", (int)key);

            javaScriptBuilder.Append("$(document).trigger(keypress);");

            remoteDriver.ExecuteScript(javaScriptBuilder.ToString());
        }

        public static long GetCursorPosition(this IWebDriver browser)
        {
            return browser.ExecuteJavaScriptAndReturn<long>("window.getSelection().getRangeAt(0).startOffset");
        }


        

    }
}
