using UIT.iDeal.Infrastructure.Configuration.Flavours;

namespace UIT.iDeal.Infrastructure.Configuration
{
    public interface ISpecifyProjectConfigurationFlavour
    {
        ISpecifyProjectConfigurationProperties SpecifyProjectConfigurationFor(ProjectFlavour projectFlavour);
    }
}