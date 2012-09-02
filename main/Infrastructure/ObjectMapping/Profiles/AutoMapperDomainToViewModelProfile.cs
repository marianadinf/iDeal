using AutoMapper;
using UIT.iDeal.Common.Extensions;
using UIT.iDeal.Common.ObjectMapping;
using UIT.iDeal.ViewModel.Tasks;

using UIT.iDeal.Common.Interfaces.ObjectMapping;

namespace UIT.iDeal.Infrastructure.ObjectMapping.Profiles
{
    public class AutoMapperDomainToViewModelProfile : Profile
    {
        protected override void Configure()
        {
            AssemblyScanner
                .GetExportedTypesFromAssemblyContaining<AddTaskForm>()
                .GetSourceTypeFromMarkerInterfaceAndDestinationTypeFromModel(typeof (IMapFromDomain<>))
                .Each(x => Mapper.CreateMap(x.SourceType, x.DestinationType).IgnoreAllNonExisting(x.SourceType, x.DestinationType));
            
        }

        
    }
}