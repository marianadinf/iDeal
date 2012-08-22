using Castle.MicroKernel.Registration;
using Castle.MicroKernel.Resolvers.SpecializedResolvers;
using Castle.Windsor;
using Castle.Windsor.Installer;
using UIT.iDeal.Common.Extensions;
using UIT.iDeal.Infrastructure.Bootstrapper.Installers;
using UIT.iDeal.Infrastructure.Bootstrapper.StartupTasks;

namespace UIT.iDeal.TestLibrary
{
    /// <summary>
    /// Replicates the ApplicationConfigurator class from the web application. It differs from ApplicationConfigurator 
    /// in that it exposes the Container as a service locator and it loads a different NHibernate Facility
    /// </summary>
    public static class TestingConfigurator
    {
        static IWindsorContainer _container;

        public static IWindsorContainer Container
        {
            get { return _container; }
        }

        /// <summary>
        /// Initialises the Castle Windsor container using FromAssembly 
        /// class to auto-discover all installers in the web assembly.
        /// Also temporarily loads installers from other assemblies. 
        /// This will be replaced by FromAssembly.InThisApplication() in Windsor 3
        /// </summary>
        public static void BuildContainer()
        {
            //InfrastructureInstaller.CacheStorageImplementation = typeof(TestingCacheStorage);
            _container = new WindsorContainer()
                .Install(FromAssembly.Containing<ControllersInstaller>());

            Container.Kernel.Resolver.AddSubResolver(new CollectionResolver(Container.Kernel));
            Container.Register(Component.For<IWindsorContainer>().Instance(Container));
        }

        /// <summary>
        /// Get all of the startup classes and call their execute method.
        /// </summary>
        public static void Start()
        {
            var tasks = Container.ResolveAll<IRunTaskAtStartup>();
            tasks.Each(task => task.Execute());
        }

        /// <summary>
        /// It is important for the application to call this method 
        /// when the application ends so that the container can be disposed.
        /// </summary>
        public static void End()
        {
            Container.Dispose();
        }
    }
}
