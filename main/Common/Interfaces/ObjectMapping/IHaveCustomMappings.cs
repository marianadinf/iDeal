using AutoMapper;

namespace UIT.iDeal.Common.Interfaces.ObjectMapping
{
    public interface IHaveCustomMappings
    {
        void CreateMappings(IConfiguration configuration);
    }
}