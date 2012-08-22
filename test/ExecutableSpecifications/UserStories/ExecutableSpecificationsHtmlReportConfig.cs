using Bddify.Processors.HtmlReporter;

namespace UIT.iDeal.Acceptance.ExecutableSpecifications.UserStories
{
    public class ExecutableSpecificationsHtmlReportConfig : DefaultHtmlReportConfiguration
    {
        public override string OutputFileName
        {
            get
            {
                return "ExecutableSpecifications.html";
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
                return "Executable Specifications";
            }
        }
    }
}
