using System;
using UIT.iDeal.Common.Interfaces.Security;
using UIT.iDeal.Infrastructure.Configuration.Flavours;

namespace UIT.iDeal.Infrastructure.Configuration
{
    public interface IPreEnvironmentFlavour
    {
        IPostEnvironmentFlavour ForEnvironment(EnvironmentFlavour environmentFlavour);
    }

    public interface IPostEnvironmentFlavour
    {
        IDefaultSecurityHelper WithDefaultSecurityHelper<T>() where T : ISecurityHelper;
    }

    public interface IDefaultSecurityHelper
    {
        IDefaultDatabaseServer WithDefaultDatabaseServer(string databaseServer);
    }

    public interface IDefaultDatabaseServer
    {
        EnvironmentConfiguration WithDefaultDatabaseFlavour(DatabaseFlavour databaseFlavour);
    }

    public class EnvironmentConfigurationBuilder :
        IPreEnvironmentFlavour,
        IPostEnvironmentFlavour,
        IDefaultSecurityHelper,
        IDefaultDatabaseServer

    {
        EnvironmentFlavour _environmentFlavour;

        string _databaseServer;

        Type _securityHelperType;

        DatabaseFlavour _databaseFlavour
;

        public static IPreEnvironmentFlavour CreateEnvironmentConfiguration()
        {
            return new EnvironmentConfigurationBuilder();
        }

        public IPostEnvironmentFlavour ForEnvironment(EnvironmentFlavour environmentFlavour)
        {
            this._environmentFlavour = environmentFlavour;
            return this;
        }

        public IDefaultSecurityHelper  WithDefaultSecurityHelper<T>() where T : ISecurityHelper
        {
            this._securityHelperType = typeof(T);
            return this;
        }

        public IDefaultDatabaseServer WithDefaultDatabaseServer(string databaseServer)
        {
            this._databaseServer = databaseServer;
            return this;
        }

        public EnvironmentConfiguration WithDefaultDatabaseFlavour(DatabaseFlavour databaseFlavour)
        {
            this._databaseFlavour = databaseFlavour;
            return this;
        }

        public static implicit operator EnvironmentConfiguration(EnvironmentConfigurationBuilder builder)
        {
            var defaultProjectConfiguration = new ProjectConfiguration(ProjectFlavour.Default, builder._securityHelperType, builder._databaseServer, builder._databaseFlavour);
            
            return new EnvironmentConfiguration(builder._environmentFlavour, defaultProjectConfiguration);
        }
    }
}
