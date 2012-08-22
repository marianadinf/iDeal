using UIT.iDeal.Common.Types;

namespace UIT.iDeal.TestLibrary.UserStories.Scenarios
{
    public class ScenarioCategory : Enumeration
    {
        protected ScenarioCategory(int id, string name)
            : base(id, name)
        {
        }

        public ScenarioCategory()
        {
        }

        public static readonly ScenarioCategory Normal = new ScenarioCategory(1, "");
        public static readonly ScenarioCategory NotImplementedSubcutaneously = new ScenarioCategory(2, " - COVERED BY BROWSER TESTING");
    }
}