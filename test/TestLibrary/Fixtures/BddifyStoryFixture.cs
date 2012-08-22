using System;
using System.Linq;
using System.ComponentModel;
using Bddify;
using Bddify.Core;

namespace UIT.iDeal.TestLibrary.Fixtures
{
    public abstract class BddifyStoryFixture<TStory> where TStory : class
    {
        public Story Bddify(object testObject, string scenarioTitle)
        {
            return testObject.Bddify<TStory>(scenarioTitle);
        }

        public Story Bddify<TScenario>(string scenarioTitle = null) where TScenario:class ,new()
        {
            var scenario = new TScenario();

            if (string.IsNullOrEmpty(scenarioTitle))
            {
                var attribute =
                    scenario
                        .GetType()
                        .GetCustomAttributes(typeof (DescriptionAttribute), false)
                        .OfType<DescriptionAttribute>()
                        .FirstOrDefault();

                if (attribute == null || string.IsNullOrEmpty((scenarioTitle = attribute.Description)))
                {
                    throw new ArgumentException("No description for the scenario has been specified");
                }

            }

            return scenario.Bddify<TStory>(scenarioTitle);
        }
    }
}
