using UIT.iDeal.Common.Interfaces.Security;
using UIT.iDeal.Infrastructure.Configuration.Flavours;

namespace UIT.iDeal.Infrastructure.Configuration
{
    public interface ISpecifyProjectConfigurationProperties
    {
        ISpecifyProjectConfigurationProperties UseDatabaseFlavour(DatabaseFlavour databaseFlavour);

        ISpecifyProjectConfigurationProperties UseDatabaseServer(string databaseServer);

        ISpecifyProjectConfigurationProperties UseSecurityHelper<T>() where T : ISecurityHelper;
    }
}