using Castle.Windsor;
using UIT.iDeal.Common.Interfaces.Data;

namespace UIT.iDeal.Infrastructure.Bootstrapper.StartupTasks
{
    /// <summary>
    /// Set the database intialisation strategy depending which implementation
    /// of IDatabaseInitialiser is registered in the Windsor IoC Container
    /// </summary>
    public class DatabaseInitialiserStrategyResolver : IRunTaskAtStartup
    {
        private readonly IWindsorContainer _container;

        public DatabaseInitialiserStrategyResolver(IWindsorContainer container)
        {
            _container = container;
        }

        public void Execute()
        {
            _container
                .Resolve<IDatabaseStrategyInitialiser>()
                .Apply();
        }

    }
}