using Castle.Windsor;
using Machine.Specifications;
using UIT.iDeal.Infrastructure.Configuration;
using UIT.iDeal.Infrastructure.Configuration.Flavours;
using UIT.iDeal.Infrastructure.Security.Helpers;

namespace UIT.iDeal.IntegrationTests.Infrastructure.Bootstrapper.Configuration
{
    [Subject("Given an EnvironmentConfigurationBuilder")]
    public class When_the_environment_configuration_builder_builds_an_environment_configuration
    {
        protected static IWindsorContainer containerWithControllers;

        protected static IEnvironmentConfiguration environmentConfiguration;

        Because of = () =>
            environmentConfiguration = EnvironmentConfigurationBuilder
                .CreateEnvironmentConfiguration()
                .ForEnvironment(EnvironmentFlavour.Tdd)
                .WithDefaultSecurityHelper<HttpContextSecurityHelper>()
                .WithDefaultDatabaseServer("localhost")
                .WithDefaultDatabaseFlavour(DatabaseFlavour.SQLServer2008);
    

        It should_build_an_environment_Configuration_ = () => environmentConfiguration.ShouldNotBeNull();

        It should_have_the_correct_Environment_Flavour = () => environmentConfiguration.EnvironmentFlavour.ShouldEqual(EnvironmentFlavour.Tdd);

        It should_have_the_correct_Security_Helper_for_any_project = () =>
                                                                   environmentConfiguration
                                                                   .SecurityHelperType(ProjectFlavour.Application)
                                                                   .ShouldEqual(typeof(HttpContextSecurityHelper));

        It should_have_the_correct_Database_Server_for_any_project = () =>
                                                                   environmentConfiguration
                                                                   .DatabaseServer(ProjectFlavour.Application)
                                                                   .ShouldEqual("localhost");

        It should_have_the_correct_Database_Flavour_for_any_project = () =>
                                                                   environmentConfiguration
                                                                   .DatabaseFlavour(ProjectFlavour.Application)
                                                                   .ShouldEqual(DatabaseFlavour.SQLServer2008);
    }

    [Subject("Given an EnvironmentConfigurationBuilder")]
    public class When_a_project_specific_configuration_for_certain_project_properties_is_added
    {
        protected static IEnvironmentConfiguration environmentConfiguration;

        Establish context = () =>
            environmentConfiguration = EnvironmentConfigurationBuilder
                .CreateEnvironmentConfiguration()
                .ForEnvironment(EnvironmentFlavour.Tdd)
                .WithDefaultSecurityHelper<HttpContextSecurityHelper>()
                .WithDefaultDatabaseServer("localhost")
                .WithDefaultDatabaseFlavour(DatabaseFlavour.SQLServer2008);

        Because of = () => environmentConfiguration
            .SpecifyProjectConfigurationFor(ProjectFlavour.ExecutableSpecifications)
            .UseDatabaseFlavour(DatabaseFlavour.SQLiteInMemory)
            .UseDatabaseServer(".");

        It should_return_the_properties_specified_for_the_project_configured = () =>
        {
            environmentConfiguration.DatabaseFlavour(ProjectFlavour.ExecutableSpecifications).ShouldEqual(DatabaseFlavour.SQLiteInMemory);
            environmentConfiguration.DatabaseServer(ProjectFlavour.ExecutableSpecifications).ShouldEqual(".");
        };

        It should_return_default_properties_if_they_are_not_specified_for_the_project_configured = () => 
            environmentConfiguration.SecurityHelperType(ProjectFlavour.ExecutableSpecifications).ShouldEqual(typeof(HttpContextSecurityHelper));
    }

    [Subject("Given an EnvironmentConfigurationBuilder")]
    public class When_the_application_project_specific_configuration_for_certain_project_properties_is_added
    {
        protected static IEnvironmentConfiguration environmentConfiguration;

        Establish context = () =>
            environmentConfiguration = EnvironmentConfigurationBuilder
                .CreateEnvironmentConfiguration()
                .ForEnvironment(EnvironmentFlavour.Tdd)
                .WithDefaultSecurityHelper<HttpContextSecurityHelper>()
                .WithDefaultDatabaseServer("localhost")
                .WithDefaultDatabaseFlavour(DatabaseFlavour.SQLServer2008);

        Because of = () => environmentConfiguration
            .SpecifyProjectConfigurationFor(ProjectFlavour.ExecutableSpecifications)
            .UseDatabaseFlavour(DatabaseFlavour.SQLiteInMemory)
            .UseDatabaseServer(".");

        It should_return_the_properties_specified_for_the_project_configured = () =>
        {
            environmentConfiguration.DatabaseFlavour(ProjectFlavour.ExecutableSpecifications).ShouldEqual(DatabaseFlavour.SQLiteInMemory);
            environmentConfiguration.DatabaseServer(ProjectFlavour.ExecutableSpecifications).ShouldEqual(".");
        };

        It should_return_default_properties_if_they_are_not_specified_for_the_project_configured = () =>
            environmentConfiguration.SecurityHelperType(ProjectFlavour.ExecutableSpecifications).ShouldEqual(typeof(HttpContextSecurityHelper));
    }
}
