using System;
using Castle.Core;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using UIT.iDeal.Common.Helpers;
using UIT.iDeal.TestLibrary.UserStories.Scenarios;

namespace UIT.iDeal.TestLibrary.Configuration.Ioc
{
    public class ScenarioWindsorInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<NotImplementedSubcutaneouslyScenario>());

            container
                .Register(WindsorHelpers
                              .GetAssemblyDescriptorFromExecuting()
                              .BasedOn(typeof (ISystemUnderTest<>))
                              .WithService.FromInterface()
                              .Configure(c => c.LifeStyle.Is(LifestyleType.Transient)),
                          Types.FromThisAssembly()
                              .BasedOn(typeof (ISystemUnderTest<>))
                              .WithService.FromInterface()
                              .LifestyleTransient(),
                          Types.FromThisAssembly()
                              .BasedOn(typeof (IControllerScenario<>))
                              .WithService.FromInterface()
                              .Configure(c => c.LifeStyle.Is(LifestyleType.Transient)));


        }

        public static Func<FromAssemblyDescriptor> TypesToRegister =
            () => WindsorHelpers.GetAssemblyDescriptorFromExecuting(); //AllTypes.FromAssemblyNamed("UIT.iDeal.Acceptance.ExecutableSpecifications");
    }
}
