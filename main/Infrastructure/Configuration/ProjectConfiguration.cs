using System;
using UIT.iDeal.Common.Interfaces.Security;
using UIT.iDeal.Infrastructure.Configuration.Flavours;

namespace UIT.iDeal.Infrastructure.Configuration
{
    public class ProjectConfiguration : ISpecifyProjectConfigurationProperties
    {
        public Type SecurityHelperType { get; protected set; }

        public string DatabaseServer { get; protected set; }

        public DatabaseFlavour? DatabaseFlavour { get; protected set; }

        public ProjectFlavour ProjectFlavour { get; protected set; }

        public ProjectConfiguration(ProjectFlavour projectFlavour)
        {
            ProjectFlavour = projectFlavour;
        }

        public ProjectConfiguration(
            ProjectFlavour projectFlavour,
            Type securityHelperType,
            string databaseServer,
            DatabaseFlavour databaseFlavour)
        {
            ProjectFlavour = projectFlavour;
            DatabaseFlavour = databaseFlavour;
            SecurityHelperType = securityHelperType;
            DatabaseServer = databaseServer;
        }

        public ISpecifyProjectConfigurationProperties UseDatabaseFlavour(DatabaseFlavour databaseFlavour)
        {
            this.DatabaseFlavour = databaseFlavour;
            return this;
        }

        public ISpecifyProjectConfigurationProperties UseDatabaseServer(string databaseServer)
        {
            this.DatabaseServer = databaseServer;
            return this;
        }

        public ISpecifyProjectConfigurationProperties UseSecurityHelper<T>() where T : ISecurityHelper
        {
            this.SecurityHelperType = typeof(T);
            return this;
        }
    }
}