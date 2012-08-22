using System;
using Machine.Specifications;
using UIT.iDeal.Common.Errors;
using UIT.iDeal.Data.EntityFrameworkProvider.Context;
using UIT.iDeal.TestLibrary.FakeObjects;

namespace UIT.iDeal.IntegrationTests.Data.EntityFrameworkProvider.Context
{
    [Subject(typeof(DataContext))]
    public class when_creating_set_for_non_existing_entity_set_property : DatabaseSpec
    {
        private static Exception _exception;

        Because of = () =>
            _exception = Catch.Exception(() => Context.CreateIncludedSet<FakeDomain>());

        It should_throw_an_Property_Not_Found_Exception = () =>
            _exception.ShouldBeOfType<PropertyNotFoundException>();

        It should_have_exception_message_No_IDbset_of_FakeDomain_found_in_DataContext = () =>
            _exception.Message.ShouldEqual("No IDbset of FakeDomain property found in DataContext");
    }
}