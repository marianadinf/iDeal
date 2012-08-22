using System.Collections.Generic;
using FluentValidation;
using FluentValidation.Results;

namespace UIT.iDeal.TestLibrary.FakeObjects
{
    public class TestCommandValidator : AbstractValidator<FakeCommand>
    {
        public TestCommandValidator()
        {
            Result = new ValidationResult(new List<ValidationFailure>());
        }

        public override ValidationResult Validate(FakeCommand instance)
        {
            return Result;
        }

        public ValidationResult Result { get; set; }
    }
}