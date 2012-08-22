using Castle.Core;
using Castle.Facilities.TypedFactory;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using UIT.iDeal.Common.Configuration;
using UIT.iDeal.Common.Extensions;
using UIT.iDeal.Common.Interfaces.Commands;
using UIT.iDeal.Infrastructure.Bootstrapper.Factories;
using UIT.iDeal.Infrastructure.Commands;

namespace UIT.iDeal.Infrastructure.Bootstrapper.Installers
{
    public class CommandHandlerInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.AddFacility<TypedFactoryFacility>()
                .Register(
                    Component.For<WindsorCommandHandlerSelector>()
                        .ImplementedBy<WindsorCommandHandlerSelector>(),
                    TypesToRegister
                        .BasedOn(typeof(ICommandHandler<>))
                        .WithService.Base()
                        .Configure(c => c.LifeStyle.Is(LifestyleType.Transient)),
                    Component.For<ICommandHandlerFactory>()
                        .AsFactory(c => c.SelectedWith<WindsorCommandHandlerSelector>())
                        .LifeStyle.Transient);
            container.Register(Component.For<ICommandInvoker>()
                                   .ImplementedBy<CommandInvoker>()
                                   .LifeStyle.Transient);


        }

        private FromAssemblyDescriptor _typesToRegister =  AllTypes.FromAssemblyNamed(ConfigSettings.CommandAssemblyName);

        public FromAssemblyDescriptor TypesToRegister
        {
            get { return _typesToRegister; }
            set { _typesToRegister = value; }
        }
    }
}