using System;
using System.Linq;
using System.Reflection;
using AutoMapper;
using UIT.iDeal.Common.Extensions;
using UIT.iDeal.Common.Interfaces.ObjectMapping;
using UIT.iDeal.Infrastructure.Bootstrapper;
using UIT.iDeal.ViewModel.Tasks;

namespace UIT.iDeal.Infrastructure.ObjectMapping.Profiles
{
    public class AutoMapperFormModelToCommandProfile : Profile
    {
        protected override void Configure()
        {
            AssemblyScanner
                .GetExportedTypesFromAssemblyContaining<AddTaskForm>()
                .GetSourceTypeFromModelAndDestinationTypeFromMakerInterface(typeof (IMapToCommand<>))
                .Each(x => Mapper.CreateMap(x.SourceType, x.DestinationType).IgnoreAllNonExisting(x.SourceType, x.DestinationType));

        }
    }
}