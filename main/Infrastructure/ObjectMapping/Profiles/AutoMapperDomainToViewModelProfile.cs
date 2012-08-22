using AutoMapper;
using UIT.iDeal.Common.Extensions;
using UIT.iDeal.Common.Interfaces.ObjectMapping;
using UIT.iDeal.ViewModel.Tasks;

namespace UIT.iDeal.Infrastructure.ObjectMapping.Profiles
{
    public class AutoMapperDomainToViewModelProfile : Profile
    {
        protected override void Configure()
        {
            AssemblyScanner
                .GetExportedTypesFromAssemblyContaining<AddTaskForm>()
                //.GetExportedTypesFromAssemblyNamed(ConfigSettings.ViewModelAssemblyName)
                .GetSourceTypeFromMarkerInterfaceAndDestinationTypeFromModel(typeof (IMapFromDomain<>))
                .Each(x => Mapper.CreateMap(x.SourceType, x.DestinationType).IgnoreAllNonExisting(x.SourceType, x.DestinationType));
            
        }

        
    }
}