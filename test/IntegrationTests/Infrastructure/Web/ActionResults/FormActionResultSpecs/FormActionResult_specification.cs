using System.Web.Mvc;
using Machine.Fakes;
using Machine.Specifications;
using MvcContrib.TestHelper;
using UIT.iDeal.Common.Errors;
using UIT.iDeal.Common.Interfaces.Forms;
using UIT.iDeal.Infrastructure.Web.ActionResults;
using UIT.iDeal.TestLibrary.FakeObjects;

namespace UIT.iDeal.IntegrationTests.Infrastructure.Web.ActionResults.FormActionResultSpecs
{
    public class FormActionResult_specification : WithFakes
    {
        protected static FormActionResult<FakeForm> Subject; 
        protected static ExecutionResult Result;
        protected static FakeForm Form = new FakeForm();
        protected static FakeController Controller;

        Establish context = () =>
        {
            Controller = new TestControllerBuilder().CreateController<FakeController>();
            Result = new ExecutionResult(Form);
            var formProcessor = An<IFormProcessor>();
            formProcessor
                .WhenToldTo(x => x.Execute(Form))
                .Return(Result);
            Subject = new FormActionResult<FakeForm>(Form, formProcessor);
            Subject.Success = new ContentResult();
            Subject.Failure = new EmptyResult();
        };
    }
}