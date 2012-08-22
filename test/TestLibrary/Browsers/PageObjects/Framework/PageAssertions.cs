using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentAssertions;
using FluentAssertions.Assertions;
using Seterlund.CodeGuard;
using UIT.iDeal.TestLibrary.Browsers.Helpers.Extensions;
using UIT.iDeal.TestLibrary.Browsers.Locators;

namespace UIT.iDeal.TestLibrary.Browsers.PageObjects.Framework
{
    public class PageAssertions
    {
        public Page Subject { get; private set; }

        protected internal PageAssertions(Page page)
        {
            Guard.That(page).IsNotNull();
            Subject = page;
        }

        public AndConstraint<PageAssertions> HaveSameTitleAsCurrentBrowserPageTitle()
        {
            return HaveSameTitleAsCurrentBrowserPageTitle(string.Empty, new object[0]);

        }

        public AndConstraint<PageAssertions> HaveSamePageIdAsCurrentBrowserPageId()
        {
            return HaveSamePageIdAsCurrentBrowserPageId(string.Empty, new object[0]);
        }

        public void BeCurrentBrowserPage()
        {
            HaveSamePageIdAsCurrentBrowserPageId().And.HaveSameTitleAsCurrentBrowserPageTitle();
        }
      
        public AndConstraint<PageAssertions> HaveSameTitleAsCurrentBrowserPageTitle(string reason, params object[] reasonArgs)
        {
            Execute.Verification.ForCondition(Subject.PageTitle == Subject.Browser.Title).BecauseOf(reason, reasonArgs).FailWith("Expected Page Title To be {0}{reason}, but found {1}.", Subject.Browser.Title, Subject.PageTitle);
            return new AndConstraint<PageAssertions>(this);
        }

        public AndConstraint<PageAssertions> HaveSamePageIdAsCurrentBrowserPageId(string reason, params object[] reasonArgs)
        {
            var currentBrowserPageId = Subject.Browser.FindElement(By.jQuery("#pageId")).GetValueFromTextBox();
            Execute.Verification.ForCondition(currentBrowserPageId == Subject.PageId).BecauseOf(reason, reasonArgs).FailWith("Expected Page Id To be {0}{reason}, but found {1}.", currentBrowserPageId, Subject.PageId);
            return new AndConstraint<PageAssertions>(this);
        }
    }
}
