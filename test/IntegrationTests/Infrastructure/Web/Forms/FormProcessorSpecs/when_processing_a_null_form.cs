using System;
using Machine.Specifications;

namespace UIT.iDeal.IntegrationTests.Infrastructure.Web.Forms.FormProcessorSpecs
{
    public class when_processing_a_null_form : form_processor_specification
    {
        protected static Exception Exception;

        Establish context = () =>
            Form = null;

        Because of = () =>
            Exception = Catch.Exception(() => Subject.Execute(Form));

        It should_throw_an_ArgumentNullException = () =>
            Exception.ShouldBeOfType<ArgumentNullException>();
    }
}