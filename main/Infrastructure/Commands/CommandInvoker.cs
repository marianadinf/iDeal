using System;
using System.Collections.Generic;
using FluentValidation;
using FluentValidation.Results;
using Seterlund.CodeGuard;
using UIT.iDeal.Common.Errors;
using UIT.iDeal.Common.Extensions;
using UIT.iDeal.Common.Interfaces.Commands;
using UIT.iDeal.Common.Interfaces.Data;

namespace UIT.iDeal.Infrastructure.Commands
{
    public class CommandInvoker : ICommandInvoker
    {
        public CommandInvoker(ICommandHandlerFactory commandHandlerFactory,
                              IValidatorFactory validatorFactory,
                              IUnitOfWork unitOfWork)
        {
            _commandHandlerFactory = commandHandlerFactory;
            _validatorFactory = validatorFactory;
            _unitOfWork = unitOfWork;
        }

        public ExecutionResult Execute<T>(T command) where T : class, ICommand
        {
            Guard.That(() => command).IsNotNull();

            _unitOfWork.BeginTransaction();

            _executionResult = new ExecutionResult(command);

            try
            {
                Validate(command);
                
                HandlerFor(command).Handle();

                _unitOfWork.CommitTransaction();
            }
            catch (ValidationException validationException)
            {
                OnError(validationException.Errors);
            }
            catch(BusinessRuleException businessRuleException)
            {
                OnError(businessRuleException);
            }
            catch (Exception fatalException)
            {
                OnError(fatalException);
            }
            finally
            {
                if (!_executionResult.IsSuccessFull)
                {
                    _unitOfWork.RollbackTransaction();
                }
            }

            return _executionResult;
        }

        private void OnError(IEnumerable<ValidationFailure> errors)
        {
            _executionResult.Merge(errors);
        }

        private void OnError(BusinessRuleException businessRuleException)
        {
            _executionResult.Add(MessageCategory.BrokenBusinessRule, businessRuleException.Message, businessRuleException.PropertyName);
        }

        private void OnError(Exception fatalException)
        {
            _executionResult.Add(MessageCategory.FatalException, fatalException.Message);
        }

        private IEnumerable<ValidationFailure> Map(BusinessRuleException businessRuleException)
        {
            return new[] {new ValidationFailure(businessRuleException.PropertyName, businessRuleException.Message)};
        }

        private void Validate<T>(T command) where T : class, ICommand
        {
            IValidator<T> validator = _validatorFactory.GetValidator<T>();
            if (validator == null)
                return;
            validator.ValidateAndThrow(command);
        }

        private ICommandHandler<T> HandlerFor<T>(T command) where T : class, ICommand
        {
            var handler = (ICommandHandler<T>)_commandHandlerFactory.HandlerForCommand(command);
            if (handler == null)
                throw new CommandHandlerNotFoundException(typeof(T));
            return handler;
        }

        
        private readonly ICommandHandlerFactory _commandHandlerFactory;
        private readonly IValidatorFactory _validatorFactory;
        private readonly IUnitOfWork _unitOfWork;
        private ExecutionResult _executionResult;
        
        
    }
}
