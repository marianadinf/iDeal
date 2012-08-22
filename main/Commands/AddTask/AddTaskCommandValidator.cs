using FluentValidation;

namespace UIT.iDeal.Commands.AddTask
{
    public class AddTaskCommandValidator : AbstractValidator<AddTaskCommand>
    {
        public AddTaskCommandValidator()
        {
            RuleFor(x => x.Description).NotNull().Length(3, 50);
        }
    }
}