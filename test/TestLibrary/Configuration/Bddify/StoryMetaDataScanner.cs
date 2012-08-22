using System;
using System.Linq;
using Bddify.Core;
using Bddify.Scanners;
using UIT.iDeal.TestLibrary.UserStories.Scenarios;

namespace UIT.iDeal.TestLibrary.Configuration.Bddify
{
    public class StoryMetaDataScanner : IStoryMetaDataScanner
    {
        public virtual StoryMetaData Scan(object testObject, Type explicityStoryType = null)
        {
            var scenario = testObject as IScenario;
            if (scenario == null)
                return null;

            var storyAttribute = GetStoryAttribute(scenario.Story);
            if (storyAttribute == null)
                return null;

            return new StoryMetaData(scenario.Story, storyAttribute);
        }

        static StoryAttribute GetStoryAttribute(Type candidateStoryType)
        {
            return (StoryAttribute)candidateStoryType.GetCustomAttributes(typeof(StoryAttribute), true).FirstOrDefault();
        }
    }
}
