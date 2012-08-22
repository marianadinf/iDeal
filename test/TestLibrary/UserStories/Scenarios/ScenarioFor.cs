using System;
using Bddify;
using NUnit.Framework;
using System.Diagnostics;
using Seterlund.CodeGuard;
using UIT.iDeal.TestLibrary.Browsers;
using UIT.iDeal.TestLibrary.Configuration.Ioc;
using UIT.iDeal.TestLibrary.Data;

namespace UIT.iDeal.TestLibrary.UserStories.Scenarios
{
    [TestFixture]
    public abstract class ScenarioFor<TScenario, TStory> : ISystemUnderTest<TScenario>, IScenario where TScenario : class
    {
        public TScenario SUT { get; set; }
        protected IDatabaseHelper Database { get; private set; }

        public Type Story { get { return typeof(TStory); } }
        public int ScenarioNumber { get; protected set; }
        public string ScenarioTitle { get; private set; }

        public virtual ScenarioCategory Category
        {
            get { return ScenarioCategory.Normal; }
        }


        protected ScenarioFor(int scenarioNumber, string scenarioTitle)
        {
            Guard.That(() => scenarioNumber).IsGreaterThan(0);
            Guard.That(() => scenarioTitle).IsNotNullOrEmpty();

            ScenarioNumber = scenarioNumber;
            ScenarioTitle = scenarioTitle;

            Database = ServiceLocator.Resolve<IDatabaseHelper>();
            Database.ResetDatabase();
            InitialiseSystemUnderTest();
        }

        [Test]
        public virtual void BddifyMe()
        {
            var scenarioTitle = string.Format("Scenario {0}: {1}{2}",
                                                 ScenarioNumber.ToString("00"),
                                                 ScenarioTitle,
                                                 Category.DisplayName);
            this.Bddify(scenarioTitle);
        }

        protected virtual void InitialiseSystemUnderTest()
        {
            SUT = ServiceLocator.Resolve<TScenario>();
        }

        public void TearDown()
        {
            var browserScenario = SUT as BrowserScenario;
            if (browserScenario != null)
            {
                browserScenario.Browser.Url = "about:blank";
            }

            if (SUT != null && SUT is IDisposable && !(SUT is BrowserScenario))
            {
                (SUT as IDisposable).Dispose();
                Debug.WriteLine("Dispose called.");
                Console.WriteLine("Dispose called.");
            }
        }

        /// <summary>
        /// 3 is many
        /// http://blogs.msdn.com/b/ploeh/archive/2008/12/08/3-is-many.aspx
        /// </summary>
        protected int Many = 3;
    }
}
