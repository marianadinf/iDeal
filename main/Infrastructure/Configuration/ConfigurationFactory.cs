using UIT.iDeal.Infrastructure.Configuration.Flavours;
using UIT.iDeal.Infrastructure.Security.Helpers;

namespace UIT.iDeal.Infrastructure.Configuration
{
    public static class ConfigurationFactory
    {
        
        public static EnvironmentConfiguration DevelopmentEnvironment()
        {
            var environmentConfiguration = EnvironmentConfigurationBuilder
                .CreateEnvironmentConfiguration()
                .ForEnvironment(EnvironmentFlavour.Tdd)
                .WithDefaultSecurityHelper<FileBasedTestSecurityHelper>()
                .WithDefaultDatabaseServer("localhost")
                .WithDefaultDatabaseFlavour(DatabaseFlavour.SQLServer2008);

            environmentConfiguration.SpecifyProjectConfigurationFor(ProjectFlavour.ExecutableSpecifications)
                .UseDatabaseFlavour(DatabaseFlavour.SQLiteInMemory)
                .UseSecurityHelper<FileBasedTestSecurityHelper>();

            environmentConfiguration.SpecifyProjectConfigurationFor(ProjectFlavour.IntegrationTests)
                .UseDatabaseFlavour(DatabaseFlavour.SQLiteInMemory)
                .UseSecurityHelper<HttpContextSecurityHelper>();

            environmentConfiguration.SpecifyProjectConfigurationFor(ProjectFlavour.FunctionalTests)
                .UseDatabaseFlavour(DatabaseFlavour.SQLiteFileSystem);

            environmentConfiguration.SpecifyProjectConfigurationFor(ProjectFlavour.ExploratoryTests)
                .UseDatabaseFlavour(DatabaseFlavour.SQLiteFileSystem);

            environmentConfiguration.SpecifyProjectConfigurationFor(ProjectFlavour.Application)
                .UseSecurityHelper<HttpContextSecurityHelper>();

            return environmentConfiguration;
        }

        public static EnvironmentConfiguration ContinuousIntegrationEnvironment()
        {
            var environmentConfiguration = EnvironmentConfigurationBuilder
                .CreateEnvironmentConfiguration()
                .ForEnvironment(EnvironmentFlavour.ContinuousIntegration)
                .WithDefaultSecurityHelper<FileBasedTestSecurityHelper>()
                .WithDefaultDatabaseServer(@".\")
                .WithDefaultDatabaseFlavour(DatabaseFlavour.SQLServer2008);

            environmentConfiguration.SpecifyProjectConfigurationFor(ProjectFlavour.ExecutableSpecifications)
                .UseDatabaseFlavour(DatabaseFlavour.SQLiteInMemory);

            environmentConfiguration.SpecifyProjectConfigurationFor(ProjectFlavour.IntegrationTests)
                .UseDatabaseFlavour(DatabaseFlavour.SQLiteInMemory)
                .UseSecurityHelper<HttpContextSecurityHelper>();

            environmentConfiguration.SpecifyProjectConfigurationFor(ProjectFlavour.FunctionalTests)
                .UseDatabaseFlavour(DatabaseFlavour.SQLiteFileSystem);

            environmentConfiguration.SpecifyProjectConfigurationFor(ProjectFlavour.ExploratoryTests)
                .UseDatabaseFlavour(DatabaseFlavour.SQLiteFileSystem);

            return environmentConfiguration;
        }

        public static EnvironmentConfiguration UatOrSystemTestEnvironment()
        {
            var environmentConfiguration = EnvironmentConfigurationBuilder
                .CreateEnvironmentConfiguration()
                .ForEnvironment(EnvironmentFlavour.UatOrSystemTest)
                .WithDefaultSecurityHelper<HttpContextSecurityHelper>()
                .WithDefaultDatabaseServer(@".\")
                .WithDefaultDatabaseFlavour(DatabaseFlavour.SQLServer2008);

            environmentConfiguration.SpecifyProjectConfigurationFor(ProjectFlavour.Application)
                .UseDatabaseFlavour(DatabaseFlavour.SQLServer2008FromWebConfig);

            return environmentConfiguration;
        }
    }
}