using AutoMapper;
using Castle.Windsor;
using Machine.Specifications;
using UIT.iDeal.Commands.AddTask;
using UIT.iDeal.Common.Builders.Entities;
using UIT.iDeal.Domain.Model;
using UIT.iDeal.Infrastructure.Bootstrapper.Installers;
using UIT.iDeal.ViewModel.Tasks;
using UIT.iDeal.TestLibrary.Extensions;

namespace UIT.iDeal.IntegrationTests.Infrastructure.Bootstrapper.Installers
{
    
    public class when_registering_AutoMapper_with_container : AutoMapper_specification
    {
        It the_mapper_configuration_should_be_valid = () =>
            Mapper.AssertConfigurationIsValid();        
    }

    public class when_mapping_from_domain_to_view_model : AutoMapper_mapping_specification<Task,TaskItemViewModel>
    {
        Establish context = () =>
                _source = new TaskBuilder();

        It should_be_able_to_map_domain_to_view_model = () =>
            _destination.ShouldBeEquivalentToModel(_source);

    }

    public class when_mapping_from_Form_Model_to_Command : AutoMapper_mapping_specification<AddTaskForm, AddTaskCommand>
    {
        Establish context = () =>
            _source = new AddTaskForm();

        It should_be_able_to_map_Form_to_Command = () =>
            _destination.ShouldBeEquivalentToModel(_source);
    }

    public class when_mapping_using_custom_mappings
    {
        Establish context = () =>
         { };

        It should_be_able_to_map_custom_mappings = () =>
        { };
        
    }
    
    #region Automapper base specification

    public class AutoMapper_mapping_specification<TSource, TDestination> : AutoMapper_specification
    {
        protected static TSource _source;
        protected static TDestination _destination;

        Because of = () =>
            _destination = Mapper.Map<TSource, TDestination>(_source);
    }



    [Subject("Given an AutoMapper container installer")]
    public class AutoMapper_specification
    {
        protected static IWindsorContainer SUT;

        Establish context = () =>
            SUT = new WindsorContainer().Install(new AutoMapperInstaller());
    }

    #endregion
}