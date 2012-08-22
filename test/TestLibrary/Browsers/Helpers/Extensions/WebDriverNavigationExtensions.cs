using FluentAssertions;
using OpenQA.Selenium;

namespace UIT.iDeal.TestLibrary.Browsers.Helpers.Extensions
{
    public static class WebDriverNavigationExtensions
    {
       public static IWebDriver NavigateTo(this IWebDriver driver, string url)
        {
            driver.Navigate().GoToUrl(url);
            WaitFor.AjaxCallsToComplete(driver);
            //WaitFor.Seconds(2);
            return driver;
        }

        public static IWebDriver NavigateTo(this IWebDriver driver, string url, string title)
        {
            driver.NavigateTo(url).Title.Should().Be(title);
            return driver;
        }

        public static IWebDriver ClickButton(this IWebDriver driver, string id)
        {
            driver.FindElement(By.Id(id)).Click();
            return driver;
        }

        public static IWebDriver NavigateByButton(this IWebDriver driver, string id, string title)
        {
            driver.ClickButton(id);
            driver.Title.Should().Be(title);
            return driver;
        }

        public static IWebDriver ClickLink(this IWebDriver driver, string linkText)
        {
            driver.FindElement(By.LinkText(linkText)).Click();
            return driver;
        }

        public static IWebDriver NavigateByLink(this IWebDriver driver, string linkText)
        {
            var link = driver.FindElement(By.LinkText(linkText));
            link.Click();
            WaitFor.AjaxCallsToComplete(driver);
            //WaitFor.Seconds(2);
            return driver;
        }
    }
}
