namespace UIT.iDeal.TestLibrary.Extensions
{
    public static class StepTextExtensions
    {
        public static string IndicateThisScenarioIsCoveredByBrowserTesting(this string stepText)
        {
            return string.Format("{0} - COVERED BY BROWSER TESTING", stepText);
        }
    }
}
