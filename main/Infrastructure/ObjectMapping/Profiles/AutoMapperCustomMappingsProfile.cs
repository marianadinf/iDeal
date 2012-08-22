using System;
using System.Linq;
using AutoMapper;
using UIT.iDeal.Common.Extensions;
using UIT.iDeal.Common.Interfaces.ObjectMapping;
using UIT.iDeal.Infrastructure.Bootstrapper;
using UIT.iDeal.ViewModel.Tasks;

namespace UIT.iDeal.Infrastructure.ObjectMapping.Profiles
{
    public class AutoMapperCustomMappingsProfile : Profile
    {
        protected override void Configure()
        {
            AssemblyScanner
                .GetExportedTypesFromAssemblyContaining<AddTaskForm>()
                .Matching(t => t.IsConcreteTypeOf(typeof (IHaveCustomMappings)))
                .Select(Activator.CreateInstance)
                .Cast<IHaveCustomMappings>()
                .Each(viewModel => viewModel.CreateMappings(Mapper.Configuration));
        }
    }
}