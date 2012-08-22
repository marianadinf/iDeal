using System;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using UIT.iDeal.Common.Interfaces.Forms;
using UIT.iDeal.Infrastructure.Web;

namespace UIT.iDeal.Infrastructure.Bootstrapper.Installers
{
    public class FormProcessorInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Component.For<IFormProcessor>()
                    .ImplementedBy<FormProcessor>()
                    .LifeStyle.Transient);
        }
    }
}