using System;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Seterlund.CodeGuard;
using UIT.iDeal.Common.Interfaces.Security;

namespace UIT.iDeal.Infrastructure.Bootstrapper.Installers
{
    //TODO: configure security helper type
    public class SecurityInstaller : IWindsorInstaller
    {
        public static string EnvironmentVar;

        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            Guard.That(() => SecurityHelperType).IsNotNull();

            container.Register(
                Component
                    .For<ISecurityHelper>()
                    .ImplementedBy(SecurityHelperType)
                );

        }

        public static Type SecurityHelperType { get; set; }
    }

}
