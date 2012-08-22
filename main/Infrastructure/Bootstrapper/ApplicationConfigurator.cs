using System.Diagnostics;
using Castle.Core;

using Castle.MicroKernel.Registration;
using Castle.MicroKernel.Resolvers.SpecializedResolvers;
using Castle.Windsor;
using Castle.Windsor.Installer;
using FizzWare.NBuilder;
using FizzWare.NBuilder.Implementation;
using UIT.iDeal.Common.Builders.Entities.Framework;
using UIT.iDeal.Common.Extensions;
using UIT.iDeal.Data.EntityFrameworkProvider.Database;
using UIT.iDeal.Infrastructure.Bootstrapper.Installers;
using UIT.iDeal.Infrastructure.Bootstrapper.StartupTasks;
using UIT.iDeal.Infrastructure.Configuration;
using UIT.iDeal.Infrastructure.Configuration.Extensions;
using UIT.iDeal.Infrastructure.Configuration.Flavours;

namespace UIT.iDeal.Infrastructure.Bootstrapper
{
    public class ApplicationConfigurator
    {
        private static IWindsorContainer _container { get; set; }

        public static void BuildContainer()
        {
            BuilderSetup.SetDefaultPropertyNamer(new PersistableEntityPropertyNamer(new ReflectionUtil()));
            _container = new WindsorContainer();

            _container = new WindsorContainer();
            _container.Kernel.Resolver.AddSubResolver(new CollectionResolver(_container.Kernel));
            
            _container.AddFacility<ConfigurationFacility>();
            var config = _container.Resolve<IEnvironmentConfiguration>();
            var possiblyInjectedProjectFlavour = Process.GetCurrentProcess().GetProjectFlavour() ?? ProjectFlavour.Application;

            EntityFrameworkDataProviderFacility.ConnectionStringProvider =
                () => config.ConnectionString(possiblyInjectedProjectFlavour);
            
            _container.AddFacility<EntityFrameworkDataProviderFacility>();

            SecurityInstaller.SecurityHelperType = config.SecurityHelperType(possiblyInjectedProjectFlavour);

            _container.Install(FromAssembly.This());
            
            _container.Register(Component.For<IWindsorContainer>().Instance(_container));
        }

        public static void Start()
        {
            var tasks = _container.ResolveAll<IRunTaskAtStartup>();
            tasks.Each(task => task.Execute());
        }

        public static void End()
        {
            _container.Dispose();
        }
    }
}