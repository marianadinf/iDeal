using Bddify.Configuration;
using Bddify.Processors.HtmlReporter;
using UIT.iDeal.TestLibrary.UserStories.Scenarios;

namespace UIT.iDeal.TestLibrary.Configuration.Bddify
{
    public static class BddifyConfiguration
    {
        public static void InitializeBddify(DefaultHtmlReportConfiguration reportConfiguration)
        {
            Configurator.Processors.TestRunner.RunsOn(s => s.Category != ScenarioCategory.NotImplementedSubcutaneously.DisplayName);
            Configurator.Scanners.StoryMetaDataScanner = () => new StoryMetaDataScanner();
            Configurator.BatchProcessors.HtmlReport.Disable();
            Configurator.BatchProcessors.Add(new HtmlReporter(reportConfiguration));
        }
    }
}
