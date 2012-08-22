using System.Data.Entity;
using Machine.Specifications;
using UIT.iDeal.Data.EntityFrameworkProvider.Context;
using UIT.iDeal.Domain.Model;

namespace UIT.iDeal.IntegrationTests.Data.EntityFrameworkProvider.Context
{
    [Subject(typeof(DataContext))]
    public class when_creating_set_for_existing_Tasks_property_in_DataContext : DatabaseSpec
    {
        private static IDbSet<Task> _taskSet;

        Because of = () =>
            _taskSet = Context.CreateIncludedSet<Task>();

        It should_return_the_value_of_the_public_property_holding_IDbSet_of_Task = () =>
            _taskSet.ShouldBeTheSameAs(Context.Tasks);
    }

    
}