using System.Reflection;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using UIT.iDeal.Common.Helpers;
using UIT.iDeal.Common.Interfaces.Queries;
using UIT.iDeal.Infrastructure.Bootstrapper.Factories;

namespace UIT.iDeal.Infrastructure.Bootstrapper.Installers
{
    public class QueryHandlerInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register
                (
                    Component.For<IQueryHandlerFactory>()
                        .ImplementedBy<QueryHandlerFactory>()
                        .LifeStyle.Transient,

                    Classes
                        .FromThisAssembly()
                        .BasedOn<IQueryHandler>()
                        .Configure(c => c.LifestyleTransient()));

        }
    }
}