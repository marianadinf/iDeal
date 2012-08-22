using System;
using AutoMapper;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using UIT.iDeal.Common.Extensions;
using UIT.iDeal.Infrastructure.ObjectMapping;
using UIT.iDeal.Infrastructure.ObjectMapping.Converters;
using UIT.iDeal.Infrastructure.ObjectMapping.Profiles;

namespace UIT.iDeal.Infrastructure.Bootstrapper.Installers
{
    public class AutoMapperInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            ConfigureMapper(container);
            RegisterComponents(container);
            InitializeAutoMappedActionResults();

            Mapper.AssertConfigurationIsValid();
        }

        private void ConfigureMapper(IWindsorContainer container)
        {
            Mapper.Initialize(x => x.ConstructServicesUsing(container.Resolve));
            Mapper.CreateMap<Enum, string>().ConvertUsing(new EnumTypeConverter());

            Mapper.AddProfile<AutoMapperDomainToViewModelProfile>();
            Mapper.AddProfile<AutoMapperFormModelToCommandProfile>();
            Mapper.AddProfile<AutoMapperCustomMappingsProfile>();
        }

        private void RegisterComponents(IWindsorContainer container)
        {
            container.Register(Component.For<IMappingEngine>().Instance(Mapper.Engine));
            container.Register(Component.For<IModelMapper>().ImplementedBy<ModelMapper>().LifestyleTransient());
        }

        private void InitializeAutoMappedActionResults()
        {
            MapperExtensions.Map = Mapper.Map;
        }
    }

    
}