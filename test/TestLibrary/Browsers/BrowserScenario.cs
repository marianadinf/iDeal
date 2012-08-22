using System;
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Web.Routing;
using Microsoft.Web.Mvc;
using MvcContrib.TestHelper;
using MvcContrib.TestHelper.Fakes;
using OpenQA.Selenium;
using UIT.iDeal.Common;
using UIT.iDeal.Common.Types;
using UIT.iDeal.TestLibrary.Browsers.Helpers;
using UIT.iDeal.TestLibrary.Browsers.Helpers.Routes;
using UIT.iDeal.TestLibrary.Browsers.PageObjects;
using UIT.iDeal.TestLibrary.Browsers.PageObjects.Framework;

namespace UIT.iDeal.TestLibrary.Browsers
{
    public class BrowserScenario : Disposable
    {
        public static string BaseUrl { get; set; }
        public static BrowserDriver Driver { get; set; }
        public IWebDriver Browser { get; private set; }

        public BrowserScenario()
        {
            Browser = Driver.CreateDriver();
        }

        public void CloseAndReopenBrowser()
        {
            Browser.Close();
            WaitFor.Seconds(2);
            BrowserDriver.KillChromeDriverProcess();
            Browser = Driver.CreateDriver();
        }


        protected override void DisposeCore()
        {
            // From SpecsFor:
            // Not all of the web drivers have implemented IDisposable correctly.  Some will dispose
            // but won't actually exit.  This wrapper fixes that inconsistent behavior. 
            Browser.Close();
            BrowserDriver.KillChromeDriverProcess();
        }

        public TDestinationPage NavigateToInitialPage<TDestinationPage, TController>(Expression<Action<TController>> action)
            where TController : Controller
            where TDestinationPage : Page, new()
        {
            var helper = new HtmlHelper(new ViewContext { HttpContext = FakeHttpContext.Root() }, new FakeViewDataContainer());
            var relativeUrl = helper.BuildUrlFromExpression(action);

            NavigateByUrl(relativeUrl);
            
           var page = new TDestinationPage { Browser = Browser };
           page.WaitForAjaxCallsToFinish();

           return page;
        }

        public void NavigateByUrl(string url)
        {
            string normalisedUrl = string.Format("{0}{1}", BaseUrl, url);
            Browser.Navigate().GoToUrl(normalisedUrl);
        }

        //public void NavigateByLink(string linkText)
        //{
        //    //Browser.NavigateByLink(linkText);
        //}

        //public void CurrentPageTitleShouldBe(string pageTitle)
        //{
        //    Browser.ShouldHavePageTitleOf(pageTitle);
        //}

        //public void CurrentPageIdShouldBe(string pageId)
        //{
        //    Browser.ShouldHavePageIdOf(pageId);
        //}

    }
}
