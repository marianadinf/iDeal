using System.Configuration;
using System.Diagnostics;
using System.Linq;
using Castle.MicroKernel.Facilities;
using Castle.MicroKernel.Registration;
using UIT.iDeal.Infrastructure.Configuration;
using UIT.iDeal.Infrastructure.Configuration.Flavours;

namespace UIT.iDeal.Infrastructure.Bootstrapper.Installers
{
    public class ConfigurationFacility : AbstractFacility
    {
        protected override void Init()
        {
            RegisterTddConfiguration();
            RegisterContinuousIntegrationBuildConfiguration();
            RegisteUatOrSystemTestBuildConfiguration();

            if (!Kernel.ResolveAll<IEnvironmentConfiguration>().Any())
            {
                throw new ConfigurationErrorsException("No Configuration was registered with the IoC Container. Has the correct Conditional Compilation Symbol been used?");
            }
        }

        [Conditional("Tdd")]
        void RegisterTddConfiguration()
        {
            var environmentConfiguration = ConfigurationFactory.DevelopmentEnvironment();
            RegisterConfiguration(environmentConfiguration);
        }

        [Conditional("ContinuousIntegration")]
        void RegisterContinuousIntegrationBuildConfiguration()
        {
            var environmentConfiguration = ConfigurationFactory.ContinuousIntegrationEnvironment();
            RegisterConfiguration(environmentConfiguration);
        }

        [Conditional("UatOrSystemTest")]
        void RegisteUatOrSystemTestBuildConfiguration()
        {
            var environmentConfiguration = ConfigurationFactory.UatOrSystemTestEnvironment();
            RegisterConfiguration(environmentConfiguration);
        }

        void RegisterConfiguration(EnvironmentConfiguration environmentConfiguration)
        {
            this.Kernel.Register(
                Component
                    .For<IEnvironmentConfiguration>()
                    .Instance(environmentConfiguration));
        }

    }

}