using System;
using System.Collections.Generic;
using Machine.Fakes;
using Machine.Specifications;
using UIT.iDeal.Common.Extensions;
using UIT.iDeal.Infrastructure.QueryHandlers;
using UIT.iDeal.TestLibrary.FakeObjects;

namespace UIT.iDeal.UnitTests.Infrastructure.QueryHandlers.Framework
{
    public class when_retrieving_strongly_typed_arguments_in_the_same_order : WithSubject<QueryHandler>
    {
        static IList<object> _manyArguments;

        Establish context = () =>
            _manyArguments = new List<object> { "a string", Guid.NewGuid(), new FakeForm() };

        Because of = () =>
            _manyArguments.Each(a => Subject.WithArgument<object>(a));

        It should_have_the_first_argument_to_be_a_string = () =>
            Subject.GetFirstArgumentOfType<String>().ShouldEqual("a string");

        It should_have_the_second_argument_to_be_a_a_Guid = () =>
            Subject.GetArgument<Guid>(2).ShouldEqual(_manyArguments[1]);

        It should_have_the_last_argument_to_be_a_a_Form = () =>
            Subject.GetLastArgumentOfType<FakeForm>().ShouldEqual(_manyArguments[2]);

    }
}