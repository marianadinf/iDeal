using System;
using Machine.Fakes;
using Machine.Specifications;
using UIT.iDeal.Infrastructure.QueryHandlers;

namespace UIT.iDeal.UnitTests.Infrastructure.QueryHandlers.Framework
{
    public class when_retrieving_an_inexisting_argument : WithSubject<QueryHandler>
    {
        static Exception _exception;

        Establish context = () =>
            Subject.WithArgument(Guid.NewGuid());

        Because of = () =>
            _exception = Catch.Exception(() => Subject.GetArgument<Guid>(5));

        It should_throw_an_argument_out_of_range_exception = () =>
            _exception.ShouldBeOfType<ArgumentOutOfRangeException>();

    }
}