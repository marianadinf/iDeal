using Castle.Core.Logging;
using FluentValidation;
using FluentValidation.Results;
using Seterlund.CodeGuard;
using UIT.iDeal.Common.Errors;
using UIT.iDeal.Common.Extensions;
using UIT.iDeal.Common.Interfaces.Commands;
using UIT.iDeal.Common.Interfaces.Forms;
using UIT.iDeal.Infrastructure.ObjectMapping;

namespace UIT.iDeal.Infrastructure.Web
{
    public class FormProcessor : IFormProcessor
    {
        public FormProcessor(IValidatorFactory validatorFactory, IModelMapper mapper, ICommandInvoker commandInvoker)
        {
            _validatorFactory = validatorFactory;
            _mapper = mapper;
            _commandInvoker = commandInvoker;
        }

        public ExecutionResult Execute<TForm>(TForm form)
            where TForm : class
        {
            Guard.That(() => form).IsNotNull();
            _executionResult = new ExecutionResult();

            Validate(form);

            if (_executionResult.IsSuccessFull)
            {
                dynamic command = _mapper.MapFormToCommand(form);
                _executionResult = _commandInvoker.Execute(command);
            }

            _logger.Info(_executionResult.ToString());
            return _executionResult;
        }

        private void Validate<T>(T form) where T : class
        {
            IValidator<T> validator = _validatorFactory.GetValidator<T>();
            if (validator == null)
                return;
            ValidationResult validationResult = validator.Validate(form);
            if (!validationResult.IsValid)
            {
                _executionResult.Merge(validationResult.Errors);
            }
        }

        

        private ILogger _logger = NullLogger.Instance;
        public ILogger Logger
        {
            get { return _logger; }
            set { _logger = value; }
        }

        private readonly IValidatorFactory _validatorFactory;
        readonly IModelMapper _mapper;
        readonly ICommandInvoker _commandInvoker;
        private ExecutionResult _executionResult;
    }
}