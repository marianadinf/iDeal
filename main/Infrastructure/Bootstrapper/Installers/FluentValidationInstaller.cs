using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using FluentValidation;
using UIT.iDeal.Common.Configuration;
using UIT.iDeal.Infrastructure.Bootstrapper.Factories;
using UIT.iDeal.Common.Extensions;
using FluentValidationAssemblyScanner = FluentValidation.AssemblyScanner;

namespace UIT.iDeal.Infrastructure.Bootstrapper.Installers
{
    public class FluentValidationInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<IValidatorFactory>().ImplementedBy<WindsorValidatorFactory>());

            FluentValidationAssemblyScanner
                .FindValidatorsInAssembly(ConfigSettings.CommandAssemblyName.GetAssembly())
                .Each(result => container.Register(Component.For(result.InterfaceType).ImplementedBy(result.ValidatorType)));
        }
    }
}