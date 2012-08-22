using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;
using UIT.iDeal.TestLibrary.Extensions;

namespace UIT.iDeal.TestLibrary.Browsers.Helpers.Extensions
{
    public static class WebElementAssertionExtensions
    {
        public static void ShouldContainText(this IEnumerable<IWebElement> elements, string text)
        {
            elements.Any(x => x.Text == text)
                .ShouldBeTrue(string.Format("The collection did not contain: {0}", text));
        }
    }
}