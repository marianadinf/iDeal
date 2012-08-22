using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using UIT.iDeal.Data.EntityFrameworkProvider.Database;
using UIT.iDeal.Infrastructure.Configuration.Flavours;

namespace UIT.iDeal.Infrastructure.Configuration
{
    public class EnvironmentConfiguration : IEnvironmentConfiguration
    {
        readonly Lazy<List<ProjectConfiguration>> _projectConfigurations = new Lazy<List<ProjectConfiguration>>();

        public EnvironmentConfiguration(EnvironmentFlavour environmentFlavour, ProjectConfiguration defaultProjectConfiguration)
        {
            EnvironmentFlavour = environmentFlavour;
            DefaultProjectConfiguration = defaultProjectConfiguration;
        }

        public EnvironmentFlavour EnvironmentFlavour { get; private set; }

        private const string DefaultConnectionStringFormat = "server={0};database=iDeal_{1};Trusted_Connection=True";

        const string WebConfigConnectionStringNameFormat = "{0}ConnectionString";

        public string ConnectionString(ProjectFlavour projectFlavour)
        {
            if (DatabaseFlavour(projectFlavour) == Flavours.DatabaseFlavour.SQLServer2008FromWebConfig)
            {
                var connectionStringName = string.Format(WebConfigConnectionStringNameFormat, projectFlavour);

                var connectionStringElement = ConfigurationManager.ConnectionStrings[connectionStringName];

                if (connectionStringElement == null)
                {
                    const string messageFormat = "The '{0}' project has been configured to use a connection string in Web.Config named '{1}', but there is no such connection string.";
                    string message = string.Format(
                        messageFormat,
                        projectFlavour,
                        connectionStringName);

                    throw new ConfigurationErrorsException(message);
                }

                return connectionStringElement.ConnectionString;

            }

            return string.Format(DefaultConnectionStringFormat, this.DatabaseServer(projectFlavour), projectFlavour);
        }

        public Type SecurityHelperType(ProjectFlavour projectFlavour)
        {
            var projectConfiguration = this.GetProjectConfiguration(projectFlavour);

            if (projectConfiguration == null || projectConfiguration.SecurityHelperType == null)
            {
                return this.DefaultProjectConfiguration.SecurityHelperType;
            }

            return projectConfiguration.SecurityHelperType;
        }

        public string DatabaseServer(ProjectFlavour projectFlavour)
        {
            var projectConfiguration = this.GetProjectConfiguration(projectFlavour);

            if (projectConfiguration == null || projectConfiguration.DatabaseServer == null)
            {
                return this.DefaultProjectConfiguration.DatabaseServer;
            }

            return projectConfiguration.DatabaseServer;
        }

        public DatabaseFlavour DatabaseFlavour(ProjectFlavour projectFlavour)
        {
            var projectConfiguration = this.GetProjectConfiguration(projectFlavour);

            if (projectConfiguration == null || projectConfiguration.DatabaseFlavour == null)
            {
                return this.DefaultProjectConfiguration.DatabaseFlavour.Value;
            }

            return projectConfiguration.DatabaseFlavour.Value;
        }

        public ProjectConfiguration DefaultProjectConfiguration { get; set; }

        public IList<ProjectConfiguration> ProjectConfigurations
        {
            get
            {
                return _projectConfigurations.Value;
            }
        }

        private ProjectConfiguration GetProjectConfiguration(ProjectFlavour projectFlavour)
        {
            var projectConfiguration = this.ProjectConfigurations.SingleOrDefault(x => x.ProjectFlavour == projectFlavour);

            return projectConfiguration;
        }

        public ISpecifyProjectConfigurationProperties SpecifyProjectConfigurationFor(ProjectFlavour projectFlavour)
        {
            var projectConfiguration = new ProjectConfiguration(projectFlavour);
            this.ProjectConfigurations.Add(projectConfiguration);
            return projectConfiguration;
        }
    }

   
}