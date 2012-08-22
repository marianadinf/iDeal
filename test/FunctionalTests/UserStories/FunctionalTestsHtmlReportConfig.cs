using Bddify.Processors.HtmlReporter;

namespace UIT.iDeal.Acceptance.FunctionalTests.UserStories
{
    public class FunctionalTestsHtmlReportConfig : DefaultHtmlReportConfiguration
    {
        public override string OutputFileName
        {
            get
            {
                return "FunctionalTests.html";
            }
        }

        public override string ReportHeader
        {
            get
            {
                return "iDeal - Real Estate Investment Tracker";
            }
        }

        public override string ReportDescription
        {
            get
            {
                return "Functional Tests";
            }
        }
    }
}