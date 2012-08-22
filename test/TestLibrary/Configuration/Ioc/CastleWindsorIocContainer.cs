using Castle.MicroKernel.Resolvers.SpecializedResolvers;
using Castle.Core;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Castle.Windsor.Installer;
using UIT.iDeal.Common.Extensions;
using UIT.iDeal.Data.EntityFrameworkProvider.Database;
using UIT.iDeal.Data.EntityFrameworkProvider.Database.Initialisation;
using UIT.iDeal.Infrastructure.Bootstrapper.Installers;
using UIT.iDeal.Infrastructure.Bootstrapper.StartupTasks;
using UIT.iDeal.Infrastructure.Configuration;
using UIT.iDeal.Infrastructure.Configuration.Flavours;
using UIT.iDeal.TestLibrary.Browsers;
using UIT.iDeal.TestLibrary.Data;

namespace UIT.iDeal.TestLibrary.Configuration.Ioc
{
    public class CastleWindsorIocContainer : IConfigureIoc
    {
        readonly ProjectFlavour _projectFlavour;

        private IWindsorContainer _container;

        public CastleWindsorIocContainer(ProjectFlavour projectFlavour)
        {
            _projectFlavour = projectFlavour;
        }

        public  IWindsorContainer Container
        {
            get { return _container; }
        }

        private void BuildContainer()
        {
            
            _container = new WindsorContainer()
                            .AddFacility<ConfigurationFacility>();

            var environmentConfiguration = _container.Resolve<IEnvironmentConfiguration>();

            SecurityInstaller.SecurityHelperType = environmentConfiguration.SecurityHelperType(_projectFlavour);

            _container
                .Install(FromAssembly.Containing<ControllersInstaller>())
                .Install(FromAssembly.This());

            #region Test Configuration for Entity Framework Data Provider

            EntityFrameworkDataProviderFacility.ConnectionStringProvider = 
                () => environmentConfiguration.ConnectionString(_projectFlavour);

            EntityFrameworkDataProviderFacility.WithContextLifeStyle(LifestyleType.Thread);

            EntityFrameworkDataProviderFacility.WithDatabaseInitialiser<AlwaysDropAndRecreateDatabaseStrategyInitialiser>();

            _container.AddFacility<EntityFrameworkDataProviderFacility>();

            DatabaseHelper.CurrentProjectFlavour = _projectFlavour;

            _container.Register(Component.For<IDatabaseHelper>().ImplementedBy<DatabaseHelper>().LifeStyle.Transient);

            #endregion

            
            _container.Register(Component.For<IWindsorContainer>().Instance(_container));
            Container.Kernel.Resolver.AddSubResolver(new CollectionResolver(Container.Kernel));

            _container.Register(Component.For<BrowserScenario>().ImplementedBy<BrowserScenario>().LifestyleSingleton());
            ServiceLocator.SetResolveFunction(_container.Resolve);
            ServiceLocator.SetReleaseAction(_container.Release);


        }

        /// <summary>
        /// Get all of the startup classes and call their execute method.
        /// </summary>
        public void Start()
        {
            BuildContainer();
            var tasks = Container.ResolveAll<IRunTaskAtStartup>();
            tasks.Each(task => task.Execute());
            
            _container.Resolve<IDatabaseHelper>().DropAndRecreateDatabase();
            
        }

        /// <summary>
        /// It is important for the application to call this method 
        /// when the application ends so that the container can be disposed.
        /// </summary>
        public void Stop()
        {
            Container.Dispose();
        }
    }
}