using UIT.iDeal.TestLibrary.Browsers.PageObjects.Framework;

namespace FluentAssertions
{
    public static class FluentAssertionsExtension
    {
        
        public static PageAssertions Should(this Page page)
        {
            return new PageAssertions(page);
        }

       
    }
}