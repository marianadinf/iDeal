using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Castle.MicroKernel.Registration;
using FluentSecurity;
using FluentSecurity.Policy;

namespace UIT.iDeal.Infrastructure.Bootstrapper.Installers
{
    class FluentSecurityInstaller :IWindsorInstaller
    {
        public void Install(Castle.Windsor.IWindsorContainer container, Castle.MicroKernel.SubSystems.Configuration.IConfigurationStore store)
        {
            container.Register(
                Component.For<IPolicyViolationHandler>()
                         .LifeStyle.Transient);
            container.ResolveAll<IPolicyViolationHandler>();
        }
    }
}
