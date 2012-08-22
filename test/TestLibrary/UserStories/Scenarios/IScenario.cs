using System;

namespace UIT.iDeal.TestLibrary.UserStories.Scenarios
{
    public interface IScenario
    {
        Type Story { get;  }
        int ScenarioNumber { get;  }
        string ScenarioTitle { get; }
    }
}