using System;
using UIT.iDeal.Infrastructure.Configuration.Flavours;

namespace UIT.iDeal.Infrastructure.Configuration
{
    public interface IEnvironmentConfiguration : ISpecifyProjectConfigurationFlavour
    {
        EnvironmentFlavour EnvironmentFlavour { get; }

        Type SecurityHelperType(ProjectFlavour application);

        string DatabaseServer(ProjectFlavour application);

        DatabaseFlavour DatabaseFlavour(ProjectFlavour application);

        string ConnectionString(ProjectFlavour projectFlavour);
    }
}
