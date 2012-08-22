using System;
using System.Linq;
using FluentValidation;
using Machine.Fakes;
using Machine.Specifications;
using UIT.iDeal.TestLibrary.FakeObjects;

namespace UIT.iDeal.IntegrationTests.Infrastructure.Web.Forms.FormProcessorSpecs
{
    public class when_processing_a_form_that_does_not_have_a_validator : form_processor_specification
    {
        protected static Exception Exception;

        Establish context = () =>
            The<IValidatorFactory>()
                .WhenToldTo(x => x.GetValidator<FakeForm>())
                .Return((IValidator<object>)null);

        Because of = () =>
            Result = Subject.Execute(Form);

        It should_return_success_status = () =>
            Result.IsSuccessFull.ShouldBeTrue();

        It should_not_throw_an_exception = () =>
            Result.Errors.Count().ShouldEqual(0);

    }
}