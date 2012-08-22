using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using UIT.iDeal.Common.Configuration;
using UIT.iDeal.Infrastructure.Bootstrapper.StartupTasks;

namespace UIT.iDeal.Infrastructure.Bootstrapper.Installers
{
    public class StartupTaskInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(AllTypes.FromAssemblyNamed(ConfigSettings.WebAssemblyName)
                .BasedOn<IRunTaskAtStartup>()
                .WithService.Base()
                .LifestyleTransient());
            
            container.Register(AllTypes.FromThisAssembly()
                .BasedOn<IRunTaskAtStartup>()
                .WithService.Base()
                .LifestyleTransient());
            
        }
    }
}
