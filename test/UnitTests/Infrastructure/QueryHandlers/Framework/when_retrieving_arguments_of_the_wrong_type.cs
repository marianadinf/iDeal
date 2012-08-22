using System;
using Machine.Fakes;
using Machine.Specifications;
using UIT.iDeal.Infrastructure.QueryHandlers;
using UIT.iDeal.TestLibrary.FakeObjects;

namespace UIT.iDeal.UnitTests.Infrastructure.QueryHandlers.Framework
{
    public class when_retrieving_arguments_of_the_wrong_type : WithSubject<QueryHandler>
    {
        static Exception _exception;

        Establish context = () =>
            Subject.WithArgument(Guid.NewGuid());

        Because of = () =>
            _exception = Catch.Exception( () =>  Subject.GetFirstArgumentOfType<FakeForm>());

        It should_throw_an_invalid_cast_exception = () =>
            _exception.ShouldBeOfType<InvalidCastException>();

        
        
    }
}